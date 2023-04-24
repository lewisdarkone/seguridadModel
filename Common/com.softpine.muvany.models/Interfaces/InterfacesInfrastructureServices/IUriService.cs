using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;

namespace com.softpine.muvany.models.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUriService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        Uri GetPaginationUri(BasePostQueryFilter filter, string actionUrl);
    }
}
