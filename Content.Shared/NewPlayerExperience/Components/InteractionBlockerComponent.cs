namespace Content.Shared.NewPlayerExperience.Components;

/// <summary>
/// This is used to mark NPE-critical objects as unable to be interacted with (unless the user is an admin).
/// </summary>
[RegisterComponent]
public sealed partial class InteractionBlockerComponent : Component;
