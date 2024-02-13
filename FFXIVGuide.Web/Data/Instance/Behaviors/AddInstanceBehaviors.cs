using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public static class InstanceBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddInstanceBehaviors(this MediatRServiceConfiguration config)
    {
        // Create behaviors
        config.AddBehavior<CreateInstanceValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateInstanceAuthorization>(ServiceLifetime.Scoped);

        // Update behaviors
        config.AddBehavior<UpdateInstanceValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateInstanceAuthorization>(ServiceLifetime.Scoped);

        // Delete behaviors
        config.AddBehavior<DeleteInstanceValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteInstanceAuthorization>(ServiceLifetime.Scoped);

        // Query behaviors

        return config;
    }
}
