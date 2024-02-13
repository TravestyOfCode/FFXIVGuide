using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public static class RouletteTypeBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddRouletteTypeBehaviors(this MediatRServiceConfiguration config)
    {
        // Create Behaviors
        config.AddBehavior<CreateRouletteTypeValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateRouletteTypeAuthorization>(ServiceLifetime.Scoped);

        // Update Behaviors
        config.AddBehavior<UpdateRouletteTypeValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateRouletteTypeAuthorization>(ServiceLifetime.Scoped);

        // Delete Behaviors
        config.AddBehavior<DeleteRouletteTypeValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteRouletteTypeAuthorization>(ServiceLifetime.Scoped);

        // Query Behaviors

        return config;
    }
}
