using LightResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FFXIVGuideAPI.Data.Errors;

public static class ResultExtensions
{
    public static Result<T> AddErrors<T>(this Result<T> result, ModelStateDictionary modelState)
    {
        if (result.IsSuccess || modelState == null)
        {
            return result;
        }

        foreach (var error in result.Errors)
        {
            foreach (var kvp in error.Metadata)
            {
                modelState.TryAddModelError(kvp.Key, kvp.Value.ToString()!);
            }
        }

        return result;
    }

    public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }

        if (result.HasError<BadRequestError>())
        {
            return controller.ValidationProblem();
        }

        if (result.HasError<NotFoundError>())
        {
            return controller.NotFound();
        }

        return controller.StatusCode(500);
    }
}
