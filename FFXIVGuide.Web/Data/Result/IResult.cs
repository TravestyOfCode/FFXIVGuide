using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FFXIVGuide.Web.Data.Result.Result;

public interface IResult
{
    public int StatusCode { get; }

    public bool WasSuccess { get; }

    public bool WasFailure { get; }

    public bool WasBadRequest { get; }

    public ModelStateDictionary Errors { get; }
}

public interface IResult<T> : IResult
{
    public T Value { get; }
}
