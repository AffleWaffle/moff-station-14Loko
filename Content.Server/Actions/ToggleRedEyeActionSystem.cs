using Content.Shared.Actions;
using Content.Shared.Light;
using Content.Shared.Actions.Components;
using Robust.Shared.GameObjects;

namespace Content.Server.Actions
{
    public sealed class ToggleRedEyeActionSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<ToggleRedEyeActionComponent, InstantActionEvent>(OnToggle);
    }

    private void OnToggle(EntityUid uid, ToggleRedEyeActionComponent comp, ref InstantActionEvent args)
    {
        var entity = args.Performer;
        if (entity == null)
            return;

        if (!HasComp<StaticLightColorComponent>(entity))
            EnsureComp<StaticLightColorComponent>(entity);
        else
            RemComp<StaticLightColorComponent>(entity);
    }
}
}