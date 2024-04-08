using LightResults;

namespace FFXIVGuideAPI.Data.Errors;

public class BadRequestError : Error
{
    public BadRequestError(string property, string message) : base("Bad Request", (property, message)) { }

    public BadRequestError(Dictionary<string, object> errors) : base("Bad Request", errors) { }
}
