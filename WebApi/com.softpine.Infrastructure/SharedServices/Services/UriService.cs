using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;

namespace com.softpine.muvany.infrastructure.SharedServices
{
    /// <summary>
    /// 
    /// </summary>
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUri"></param>
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public Uri GetPaginationUri(BasePostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}/?{nameof(filter.PageSize)}={filter.PageSize}&{nameof(filter.PageNumber)}={filter.PageNumber}";
            return new Uri(baseUrl);
        }
    }
}
