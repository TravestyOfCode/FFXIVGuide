using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FFXIVGuide.Web.Data.Results;

public class Result : IResult
{
    public int StatusCode { get; private set; }

    public bool WasSuccess => StatusCode >= 200 && StatusCode <= 299;

    public bool WasFailure => !WasSuccess;

    public bool WasBadRequest => StatusCode == 400;

    public ModelStateDictionary Errors { get; } = new ModelStateDictionary();

    public Result(int statusCode)
    {
        StatusCode = statusCode;
    }

    public static Result Ok() => new(200);
    public static Result<T> Ok<T>() => new(200, default);
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

public static class ResultExtensions
{
    public static void AddErrors(this ModelStateDictionary ms, Result result)
    {
        if (result.WasFailure)
        {
            foreach (var property in result.Errors)
            {
                foreach (var error in property.Value.Errors)
                {
                    if (error.ErrorMessage != null)
                    {
                        ms.AddModelError(property.Key, error.ErrorMessage);
                    }
                    if (error.Exception != null)
                    {
                        ms.TryAddModelException(property.Key, error.Exception);
                    }
                }
            }
        }
    }
}
