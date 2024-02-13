using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public static class NoteBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddNoteBehaviors(this MediatRServiceConfiguration config)
    {
        // Create Behaviors
        config.AddBehavior<CreateNoteValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateNoteAuthorization>(ServiceLifetime.Scoped);

        // Update Behaviors
        config.AddBehavior<UpdateNoteValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateNoteAuthorization>(ServiceLifetime.Scoped);

        // Delete Behaviors
        config.AddBehavior<DeleteNoteValidation>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteNoteAuthorization>(ServiceLifetime.Scoped);

        // Query Behaviors

        return config;
    }
}
