using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public static class EncounterBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddEncounterBehaviors(this MediatRServiceConfiguration config)
    {
        // Create Behaviors
        config.AddBehavior<CreateEncounterValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateEncounterAuthorization>(ServiceLifetime.Scoped);

        // Update Behaviors
        config.AddBehavior<UpdateEncounterValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateEncounterAuthorization>(ServiceLifetime.Scoped);

        // Delete Behaviors
        config.AddBehavior<DeleteEncounterValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteEncounterAuthorization>(ServiceLifetime.Scoped);

        // Query Behaviors

        return config;
    }
}
