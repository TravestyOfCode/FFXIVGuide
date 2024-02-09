using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public static class InstanceBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddInstanceBehaviors(this MediatRServiceConfiguration config)
    {
        // Create behaviors
        config.AddBehavior<CreateInstanceAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateInstanceValidation>(ServiceLifetime.Scoped);

        // Update behaviors
        config.AddBehavior<UpdateInstanceAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateInstanceValidation>(ServiceLifetime.Scoped);

        // Delete behaviors
        config.AddBehavior<DeleteInstanceAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteInstanceValidation>(ServiceLifetime.Scoped);

        // Query behaviors

        return config;
    }
}
