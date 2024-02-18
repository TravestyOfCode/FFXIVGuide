namespace FFXIVGuide.Web.Data.Results;

public class PagedValue<T> : IPagedInfo
{
    public double TotalPages { get; set; }

    public int Page { get; set; }

    public int PerPage { get; set; }

    public string SortBy { get; set; }

    public SortOrder SortOrder { get; set; }

    public IEnumerable<T> Values { get; set; }

    public PagedValue()
    {
        TotalPages = 0;
        Page = 1;
        PerPage = 10;
        SortBy = string.Empty;
        SortOrder = SortOrder.ASC;
        Values = new List<T>();
    }
}
