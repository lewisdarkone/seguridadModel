using System.Linq.Expressions;
using System.Reflection;
using Ardalis.Specification;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.models.Specification;

// See https://github.com/ardalis/Specification/issues/53
/// <summary>
/// 
/// </summary>
public static class SpecificationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static ISpecificationBuilder<T> SearchBy<T>(this ISpecificationBuilder<T> query, BaseFilter filter) =>
        query
            .SearchByKeyword(filter.Keyword)
            .AdvancedSearch(filter.AdvancedSearch);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static ISpecificationBuilder<T> PaginateBy<T>(this ISpecificationBuilder<T> query, PaginationFilter filter)
    {
        if (filter.PageNumber <= 0)
        {
            filter.PageNumber = 1;
        }

        if (filter.PageSize <= 0)
        {
            filter.PageSize = 10;
        }

        if (filter.PageNumber > 1)
        {
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize);
        }

        return query
            .Take(filter.PageSize)
            .OrderBy(filter.OrderBy);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static IOrderedSpecificationBuilder<T> SearchByKeyword<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string? keyword) =>
        specificationBuilder.AdvancedSearch(new Search { Keyword = keyword });

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static IOrderedSpecificationBuilder<T> AdvancedSearch<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Search? search)
    {
        if (!string.IsNullOrEmpty(search?.Keyword))
        {
            if (search.Fields?.Any() is true)
            {
                // search seleted fields (can contain deeper nested fields)
                foreach (string field in search.Fields)
                {
                    var paramExpr = Expression.Parameter(typeof(T));

                    Expression propertyExpr = paramExpr;
                    foreach (string member in field.Split('.'))
                    {
                        propertyExpr = Expression.PropertyOrField(propertyExpr, member);
                    }

                    specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr, search.Keyword);
                }
            }
            else
            {
                // search all fields (only first level)
                foreach (var property in typeof(T).GetProperties()
                    .Where(prop => (Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType) is { } propertyType
                        && !propertyType.IsEnum
                        && Type.GetTypeCode(propertyType) != TypeCode.Object))
                {
                    var paramExpr = Expression.Parameter(typeof(T));
                    var propertyExpr = Expression.Property(paramExpr, property);

                    specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr, search.Keyword);
                }
            }
        }

        return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
    }

    private static void AddSearchPropertyByKeyword<T>(this ISpecificationBuilder<T> specificationBuilder, Expression propertyExpr, ParameterExpression paramExpr, string keyword)
    {
        if (propertyExpr is not MemberExpression memberExpr || memberExpr.Member is not PropertyInfo property)
        {
            throw new ArgumentException(ApiConstants.Messages.PropertyExpressionError, nameof(propertyExpr));
        }

        // Generate lambda [ x => x.Property ] for string properties
        // or [ x => ((object)x.Property) == null ? null : x.Property.ToString() ] for other properties
        Expression selectorExpr =
            property.PropertyType == typeof(string)
                ? propertyExpr
                : Expression.Condition(
                    Expression.Equal(
                        Expression.Convert(propertyExpr, typeof(object)),
                        Expression.Constant(null, typeof(object))),
                    Expression.Constant(null, typeof(string)),
                    Expression.Call(propertyExpr, "ToString", null, null));

        var selector = Expression.Lambda<Func<T, string>>(selectorExpr, paramExpr);

        ((List<SearchExpressionInfo<T>>)specificationBuilder.Specification.SearchCriterias)
            .Add(new SearchExpressionInfo<T>(selector, $"%{keyword}%", 1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specificationBuilder"></param>
    /// <param name="orderByFields"></param>
    /// <returns></returns>
    public static IOrderedSpecificationBuilder<T> OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string[]? orderByFields)
    {
        if (orderByFields is not null)
        {
            foreach (var field in ParseOrderBy(orderByFields))
            {
                var paramExpr = Expression.Parameter(typeof(T));

                Expression propertyExpr = paramExpr;
                foreach (string member in field.Key.Split('.'))
                {
                    propertyExpr = Expression.PropertyOrField(propertyExpr, member);
                }

                var keySelector = Expression.Lambda<Func<T, object?>>(
                    Expression.Convert(propertyExpr, typeof(object)),
                    paramExpr);

                ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<T>(keySelector, field.Value));
            }
        }

        return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
    }

    private static Dictionary<string, OrderTypeEnum> ParseOrderBy(string[] orderByFields) =>
        new(orderByFields.Select((orderByfield, index) =>
        {
            string[] fieldParts = orderByfield.Split(' ');
            string field = fieldParts[0];
            bool descending = fieldParts.Length > 1 && fieldParts[1].StartsWith("Desc", StringComparison.OrdinalIgnoreCase);
            var orderBy = index == 0
                ? descending ? OrderTypeEnum.OrderByDescending
                                : OrderTypeEnum.OrderBy
                : descending ? OrderTypeEnum.ThenByDescending
                                : OrderTypeEnum.ThenBy;

            return new KeyValuePair<string, OrderTypeEnum>(field, orderBy);
        }));
}
