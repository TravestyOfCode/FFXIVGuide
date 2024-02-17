namespace FFXIVGuide.Web.Data.Result.Result;

public interface IPagedResult : IPagedQuery
{
    public double TotalPages { get; }
}
