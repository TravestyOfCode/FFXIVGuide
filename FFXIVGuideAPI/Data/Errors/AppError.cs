using LightResults;

namespace FFXIVGuideAPI.Data.Errors;

public static class AppError
{
    public static Result<T> BadRequest<T>(string property, string message)
    {
        return Result.Fail<T>(new BadRequestError(property, message));

    }

    public static Result<T> BadRequest<T>(Dictionary<string, object> errors)
    {
        return Result.Fail<T>(new BadRequestError(errors));
    }

    public static Result<T> NotFound<T>()
    {
        return Result.Fail<T>(new NotFoundError());
    }

    public static Result<T> ServerError<T>()
    {
        return Result.Fail<T>(new InternalServerError());
    }
}
