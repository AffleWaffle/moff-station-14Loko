using Content.Shared.Actions;
using Content.Shared.Light;
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

            if (args.Pressed)
            {
                EnsureComp<StaticLightColorComponent>(entity.Value);
            }
            else
            {
                RemComp<StaticLightColorComponent>(entity.Value);
            }
        }
    }
}