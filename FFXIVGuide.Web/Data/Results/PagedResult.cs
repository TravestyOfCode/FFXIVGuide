namespace FFXIVGuide.Web.Data.Results;

public class PagedResult<T> : Result<PagedValue<T>>
{
    public PagedResult(int statusCode, IPagedQuery query) : base(statusCode)
    {
        Value = new PagedValue<T>()
        {
            Page = query.Page,
            PerPage = query.PerPage,
            SortBy = query.SortBy,
            SortOrder = query.SortOrder,
            TotalPages = 0,
            Values = new List<T>()
        };
    }

    public PagedResult(int statusCode, List<T> value, IPagedQuery query, double totalCount) : base(statusCode)
    {
        Value = new PagedValue<T>()
        {
            Page = query.Page,
            PerPage = query.PerPage,
            SortBy = query.SortBy,
            SortOrder = query.SortOrder,
            TotalPages = Math.Ceiling(totalCount / query.PerPage),
            Values = value
        };
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