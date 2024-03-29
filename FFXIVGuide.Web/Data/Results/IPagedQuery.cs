﻿namespace FFXIVGuide.Web.Data.Results;

public enum SortOrder { ASC, DESC }

public interface IPagedQuery
{
    public int Page { get; }

    public int PerPage { get; }

    public string SortBy { get; }

    public SortOrder SortOrder { get; }
}