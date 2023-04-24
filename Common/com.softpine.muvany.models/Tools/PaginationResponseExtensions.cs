using Ardalis.Specification;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.Tools;

/// <summary>
/// 
/// </summary>
public static class PaginationResponseExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="repository"></param>
    /// <param name="spec"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        this IReadRepositoryBase<T> repository, ISpecification<T, TDestination> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto
    {
        var list = await repository.ListAsync(spec, cancellationToken);
        int count = await repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
    }
}
