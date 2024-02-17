namespace FFXIVGuide.Web.Data;

public interface IPagedResult : IPagedQuery
{
    public double TotalPages { get; }
}
