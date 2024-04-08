using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuideAPI.Models.RouletteType.Behaviors;

internal static class AddBehaviors
{
    public static MediatRServiceConfiguration AddRouletteTypeBehaviors(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<CreateValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateValidation>(ServiceLifetime.Scoped);

        return config;
    }
}
