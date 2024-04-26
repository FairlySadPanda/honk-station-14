using Content.Shared.Objectives.Components;
using Content.Shared.Scurret.Scavenger;

namespace Content.Server.Scurret.Scavenger;

public sealed class ConsumeNutrientsObjectiveSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ConsumeNutrientsConditionComponent, ObjectiveGetProgressEvent>(OnConsumeNutrientsGetProgress);
    }

    private static void OnConsumeNutrientsGetProgress(EntityUid uid, ConsumeNutrientsConditionComponent comp, ref ObjectiveGetProgressEvent args)
    {
        args.Progress = comp.NutrientsConsumed / comp.NutrientsRequired;
    }
}
