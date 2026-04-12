using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;

namespace Content.Shared.Actions.Components
{
    // NetworkedComponent makes it visible to the server build
    [RegisterComponent]
    public sealed partial class ToggleRedEyeActionComponent : Component
    {
        // Dummy property so the network serializer has something to serialize
        [DataField("dummy")]
        public bool Dummy { get; set; } = false;
    }

    // Optional: create a dummy state class for networking
    [Serializable, NetSerializable]
    public sealed class ToggleRedEyeActionState : ComponentState
    {
        public readonly bool Dummy;

        public ToggleRedEyeActionState(bool dummy)
        {
            Dummy = dummy;
        }
    }
}