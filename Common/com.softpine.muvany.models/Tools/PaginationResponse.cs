namespace com.softpine.muvany.models.Tools;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationResponse<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="count"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    public PaginationResponse(List<T> Data, int count, int page, int pageSize)
    {
        Data = Data;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<T> Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// 
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;
}
