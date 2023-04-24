#pragma warning disable 1591
using System.ComponentModel.DataAnnotations.Schema;

namespace com.softpine.muvany.models.Contracts;

/// <summary>
/// 
/// </summary>
public abstract class AuditableEntity : IAuditableEntity, ISoftDelete, IEntity
{
    [NotMapped]
    public string CreatedBy { get; set; }

    [NotMapped]
    public DateTime CreatedOn { get; private set; }

    [NotMapped]
    public string LastModifiedBy { get; set; }

    [NotMapped]
    public DateTime? LastModifiedOn { get; set; }

    [NotMapped]
    public DateTime? DeletedOn { get; set; }

    [NotMapped]
    public string? DeletedBy { get; set; }

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();
   
    protected AuditableEntity()
    {
        CreatedOn = DateTime.Now;
        LastModifiedOn = DateTime.Now;
    }
}


