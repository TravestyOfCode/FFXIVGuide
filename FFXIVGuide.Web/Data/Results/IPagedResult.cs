namespace FFXIVGuide.Web.Data.Results;

public interface IPagedResult : IPagedQuery
{
    public double TotalPages { get; }
}
