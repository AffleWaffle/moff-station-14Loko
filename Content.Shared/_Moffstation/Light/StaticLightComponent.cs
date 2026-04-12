using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;
using Robust.Shared.Maths;
using System.Collections.Generic;

namespace Content.Shared.Light
{
    [RegisterComponent]
    public sealed partial class StaticLightColorComponent : Component
    {
        [DataField]
        public Color Color = new(0xDD, 0x20, 0x0B, 0xFF);

        [DataField]
        public List<int>? Layers;

        public Color OriginalLightColor;
        public Dictionary<int, Color>? OriginalLayerColors;
    }
}