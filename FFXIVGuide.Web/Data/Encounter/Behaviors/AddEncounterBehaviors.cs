using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public static class EncounterBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddEncounterBehaviors(this MediatRServiceConfiguration config)
    {
        // Create Behaviors
        config.AddBehavior<CreateEncounterAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateEncounterValidation>(ServiceLifetime.Scoped);

        // Update Behaviors
        config.AddBehavior<UpdateEncounterAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateEncounterAuthorization>(ServiceLifetime.Scoped);

        // Delete Behaviors
        config.AddBehavior<DeleteEncounterAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteEncounterAuthorization>(ServiceLifetime.Scoped);

        // Query Behaviors

        return config;
    }
}
