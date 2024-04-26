using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared.Scurret.Scavenger;

[RegisterComponent]
public sealed partial class ScurretScavengerComponent : Component
{
    [DataField]
    public string RoleIntroSfx = "";

    [DataField]
    public ProtoId<AntagPrototype> AntagProtoId = "ScurretScavenger";

    [DataField]
    public string RoleBriefing = "";

    [DataField]
    public string RoleGreeting = "";

    [DataField]
    public string HibernateSpriteStateId = "";

    [DataField]
    public string TooFarFromHibernationSpot = "";

    [DataField]
    public string NotEnoughNutrientsMessage = "";
}
