﻿#pragma warning disable 1591
namespace com.softpine.muvany.models.Contracts;

public interface IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
