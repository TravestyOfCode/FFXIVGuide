using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FFXIVGuide.Web.Data;

public class Result : IResult
{
    public int StatusCode { get; private set; }

    public bool WasSuccess => StatusCode >= 200 && StatusCode <= 299;

    public bool WasFailure => !WasSuccess;

    public ModelStateDictionary Errors { get; } = new ModelStateDictionary();

    public Result(int statusCode)
    {
        StatusCode = statusCode;
    }

    public static Result Ok() => new(200);
    public static Result<T> Ok<T>(T value) => new(200, value);

    public static Result Created() => new(201);
    public static Result<T> Created<T>(T value) => new(201, value);

    public static Result BadRequest() => new(400);
    public static Result<T> BadRequest<T>() => new(400, default);
    public static Result BadRequest(string property, string error)
    {
        var result = new Result(400);
        result.Errors.AddModelError(property, error);
        return result;
    }
    public static Result<T> BadRequest<T>(string property, string error)
    {
        var result = new Result<T>(400);
        result.Errors.AddModelError(property, error);
        return result;
    }

    public static Result Forbidden() => new(403);
    public static Result<T> Forbidden<T>() => new(403, default);

    public static Result NotFound() => new(404);
    public static Result<T> NotFound<T>() => new(404, default);

    public static Result ServerError() => new(500);
    public static Result<T> ServerError<T>() => new(500, default);
}

public class Result<T> : Result
{
    public Result(int statusCode) : base(statusCode)
    {
    }

    public Result(int statusCode, T value) : base(statusCode)
    {
        Value = value;
    }

    public T Value { get; set; }
}
