using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace FFXIVGuide.Web.Data;

public abstract class PagedRequest<T> : IRequest<PagedResult<T>>, IPagedQuery
{
    private int _Page;
    public int Page
    {
        get => _Page;
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(Page));
            }

            _Page = value;
        }
    }

    private int _PerPage;
    public int PerPage
    {
        get => _PerPage;
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(PerPage));
            }

            _PerPage = value;
        }
    }

    public string SortBy { get; set; }

    public SortOrder SortOrder { get; set; }

    public PagedRequest()
    {
        Page = 1;
        PerPage = 10;
        SortBy = string.Empty;
        SortOrder = SortOrder.ASC;
    }
}

public static class PagedRequestExtensions
{
    public static IQueryable<T> AsPagedQuery<T>(this IQueryable<T> query, IPagedQuery page)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(page);

        // Sort if we have a sort by value
        if (!page.SortBy.IsNullOrEmpty())
        {
            query = page.SortOrder switch
            {
                SortOrder.DESC => query.OrderByDescending(p => EF.Property<object>(p!, page.SortBy)),
                _ => query.OrderBy(p => EF.Property<object>(p!, page.SortBy)),
            };
        }

        return query.Skip((page.Page - 1) * page.PerPage).Take(page.PerPage);
    }
}