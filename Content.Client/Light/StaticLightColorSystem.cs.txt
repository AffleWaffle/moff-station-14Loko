using Content.Shared.Light;
using Robust.Client.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;
using System.Linq;

namespace Content.Client.Light
{
    public sealed class StaticLightColorSystem : EntitySystem
    {
        [Dependency] private readonly SharedPointLightSystem _lights = default!;
        [Dependency] private readonly SpriteSystem _sprite = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<StaticLightColorComponent, ComponentStartup>(OnStartup);
            SubscribeLocalEvent<StaticLightColorComponent, ComponentShutdown>(OnShutdown);
        }

        private void OnStartup(EntityUid uid, StaticLightColorComponent comp, ComponentStartup args)
        {
            if (!TryComp<PointLightComponent>(uid, out var light) ||
                !TryComp<SpriteComponent>(uid, out var sprite))
                return;

            comp.OriginalLightColor = light.Color;
            comp.OriginalLayerColors = new();

            var layerCount = sprite.AllLayers.Count();

            if (comp.Layers == null)
            {
                comp.Layers = new();
                for (var i = 0; i < layerCount; i++)
                {
                    if (sprite[i] is SpriteComponent.Layer layer &&
                        layer.ShaderPrototype == "unshaded")
                    {
                        comp.Layers.Add(i);
                        comp.OriginalLayerColors[i] = layer.Color;
                    }
                }
            }
            else
            {
                foreach (var index in comp.Layers)
                {
                    if (index < layerCount)
                        comp.OriginalLayerColors[index] = sprite[index].Color;
                }
            }

            _lights.SetColor(uid, comp.Color, light);

            foreach (var (layer, _) in comp.OriginalLayerColors)
            {
                _sprite.LayerSetColor((uid, sprite), layer, comp.Color);
            }
        }

        private void OnShutdown(EntityUid uid, StaticLightColorComponent comp, ComponentShutdown args)
        {
            if (!TryComp<PointLightComponent>(uid, out var light) ||
                !TryComp<SpriteComponent>(uid, out var sprite))
                return;

            _lights.SetColor(uid, comp.OriginalLightColor, light);

            if (comp.OriginalLayerColors == null)
                return;

            foreach (var (layer, color) in comp.OriginalLayerColors)
            {
                _sprite.LayerSetColor((uid, sprite), layer, color);
            }
        }
    }
}