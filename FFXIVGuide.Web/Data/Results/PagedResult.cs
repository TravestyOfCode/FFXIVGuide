namespace FFXIVGuide.Web.Data.Results;

public class PagedResult<T> : Result<List<T>>, IPagedResult
{
    public int Page { get; set; }

    public int PerPage { get; set; }

    public string SortBy { get; set; }

    public SortOrder SortOrder { get; set; }

    public double TotalPages { get; set; }

    public PagedResult(int statusCode, IPagedQuery query) : base(statusCode)
    {
        Page = query.Page;
        PerPage = query.PerPage;
        SortBy = query.SortBy;
        SortOrder = query.SortOrder;
        TotalPages = 0;
    }

    public PagedResult(int statusCode, List<T> value, IPagedQuery query, double totalCount) : base(statusCode, value)
    {
        Page = query.Page;
        PerPage = query.PerPage;
        SortBy = query.SortBy;
        SortOrder = query.SortOrder;
        TotalPages = Math.Ceiling(totalCount / query.PerPage);
    }

    public static PagedResult<T> Ok(List<T> value, IPagedQuery query, double totalCount) => new(200, value, query, totalCount);

    public static PagedResult<T> BadRequest(IPagedQuery query) => new(400, query);
    public static PagedResult<T> BadRequest(string property, string error, IPagedQuery query)
    {
        var result = new PagedResult<T>(400, query);
        result.Errors.AddModelError(property, error);
        return result;
    }

    public static PagedResult<T> Forbidden(IPagedQuery query) => new(403, query);

    public static PagedResult<T> NotFound(IPagedQuery query) => new(404, query);

    public static PagedResult<T> ServerError(IPagedQuery query) => new(500, query);
}
