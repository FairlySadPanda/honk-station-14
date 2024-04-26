using Content.Shared.Scurret.Scavenger;
using Robust.Client.GameObjects;

namespace Content.Client.Scurret.Scavenger;

public sealed class HibernateSpriteSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<EntityHasHibernated>(OnHibernateEvent);
    }

    public void OnHibernateEvent(EntityHasHibernated args)
    {
        if (!TryGetEntity(args.Hibernator, out var uid))
        {
            return;
        }

        if (!TryComp<SpriteComponent>(uid, out var comp))
        {
            return;
        }

        comp.LayerSetState(0, args.SpriteStateId);
    }
}
