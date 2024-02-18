namespace FFXIVGuide.Web.Data.Results;

public interface IPagedInfo : IPagedQuery
{
    public double TotalPages { get; }
}
