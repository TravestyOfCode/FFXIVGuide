using LightResults;

namespace FFXIVGuideAPI.Data.Errors;

public class NotFoundError : Error
{
    public NotFoundError() : base("Not Found")
    {

    }
}
