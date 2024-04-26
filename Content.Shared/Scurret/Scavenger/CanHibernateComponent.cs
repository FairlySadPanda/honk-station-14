using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Scurret.Scavenger;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CanHibernateComponent : Component
{
    [DataField]
    public EntProtoId EepyAction = "ActionEepy";

    [DataField]
    public EntityUid? EepyActionEntity;

    [DataField, AutoNetworkedField]
    public string NotEnoughNutrientsMessage = "";

    [DataField, AutoNetworkedField]
    public string TooFarFromHibernationSpot = "";

    [DataField, AutoNetworkedField]
    public string SpriteStateId = "";
}

public sealed partial class EepyActionEvent : InstantActionEvent
{
}

[Serializable, NetSerializable]
public sealed class EntityHasHibernated(NetEntity hibernator, string spriteStateId) : EntityEventArgs
{
    public NetEntity Hibernator = hibernator;
    public string SpriteStateId = spriteStateId;
}
