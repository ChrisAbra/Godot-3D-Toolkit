namespace Godot3dToolkit;
[GlobalClass]
public partial class Damage : InteractableResource
{
    public override uint PhysicsLayer
    {
        get => 1 << 4;
        set { return; }
    }

}