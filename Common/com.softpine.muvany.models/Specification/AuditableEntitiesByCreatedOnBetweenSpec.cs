using Ardalis.Specification;
using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.models.Specification;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AuditableEntitiesByCreatedOnBetweenSpec<T> : Specification<T>
    where T : AuditableEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="until"></param>
    public AuditableEntitiesByCreatedOnBetweenSpec(DateTime from, DateTime until) =>
        Query.Where(e => e.CreatedOn >= from && e.CreatedOn <= until);
}
