

using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UserListFilter : PaginationFilter
{
    /// <summary>
    /// 
    /// </summary>
    public bool? IsActive { get; set; }
}
