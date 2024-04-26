using Content.Shared.Objectives.Components;
using Content.Shared.Scurret.Scavenger;

namespace Content.Server.Scurret.Scavenger;

public sealed class HibernateObjectiveSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HibernateConditionComponent, ObjectiveGetProgressEvent>(OnHibernateGetProgress);
    }

    private static void OnHibernateGetProgress(EntityUid uid, HibernateConditionComponent comp, ref ObjectiveGetProgressEvent args)
    {
        args.Progress = comp.Hibernated ? 1.0f : 0.0f;
    }
}
