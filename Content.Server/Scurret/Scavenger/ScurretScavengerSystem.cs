using Content.Server.Chat.Managers;
using Content.Server.GameTicking.Components;
using Content.Server.GenericAntag;
using Content.Server.Interaction;
using Content.Server.Roles;
using Content.Server.StationEvents.Events;
using Content.Shared.Mind;
using Content.Shared.Movement.Components;
using Content.Shared.Nutrition.EntitySystems;
using Content.Shared.Roles;
using Content.Shared.Scurret.Scavenger;
using Robust.Shared.Audio;

namespace Content.Server.Scurret.Scavenger;

public sealed class ScurretScavengerSystem : EntitySystem
{
    [Dependency] private readonly RoleSystem _role = default!;
    [Dependency] private readonly IChatManager _chatMan = default!;
    [Dependency] private readonly SharedMindSystem _mind = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ScurretScavengerComponent, GenericAntagCreatedEvent>(OnInit);
        SubscribeLocalEvent<ScurretScavengerComponent, HungerModifiedEvent>(OnHungerModified);
    }

    private void OnInit(EntityUid uid, ScurretScavengerComponent scavengerComponent, GenericAntagCreatedEvent args)
    {
        var mind = args.Mind;

        if (mind.Session == null)
            return;

        var session = mind.Session;
        _role.MindAddRole(args.MindId, new RoleBriefingComponent
        {
            Briefing = Loc.GetString(scavengerComponent.RoleBriefing)
        }, mind);
        _role.MindAddRole(args.MindId, new ScurretScavengerRoleComponent()
        {
            PrototypeId = scavengerComponent.AntagProtoId
        }, mind);
        _role.MindPlaySound(args.MindId, new SoundPathSpecifier(scavengerComponent.RoleIntroSfx), mind);
        _chatMan.DispatchServerMessage(session, Loc.GetString(scavengerComponent.RoleGreeting));
    }

    private void OnHungerModified(EntityUid uid, ScurretScavengerComponent comp, HungerModifiedEvent args)
    {
        if (!_mind.TryGetObjectiveComp<ConsumeNutrientsConditionComponent>(uid, out var nutrientsCondition) || !(args.Amount > 0))
            return;

        nutrientsCondition.NutrientsConsumed += args.Amount;

        if (nutrientsCondition.NutrientsRequired <= nutrientsCondition.NutrientsConsumed)
        {
            var hibernateComponent = EnsureComp<CanHibernateComponent>(uid);
            hibernateComponent.SpriteStateId = comp.HibernateSpriteStateId;
            hibernateComponent.TooFarFromHibernationSpot = comp.TooFarFromHibernationSpot;
            hibernateComponent.NotEnoughNutrientsMessage = comp.NotEnoughNutrientsMessage;
        }
    }
}

[RegisterComponent, ExclusiveAntagonist]
public sealed partial class ScurretScavengerRoleComponent : AntagonistRoleComponent;

[RegisterComponent]
public sealed partial class ScurretScavengerSpawnRuleComponent : Component;

public sealed class ScurretScavengerSpawnRule : StationEventSystem<ScurretScavengerSpawnRuleComponent>
{
    protected override void Started(EntityUid uid, ScurretScavengerSpawnRuleComponent comp, GameRuleComponent gameRule, GameRuleStartedEvent args)
    {
        base.Started(uid, comp, gameRule, args);

        TryFindRandomTile(out _, out _, out _, out var coords);
        Sawmill.Info($"Creating scurret scavenger spawn point at {coords}");
        Spawn("SpawnPointGhostScurretScavenger", coords);
    }
}
