namespace Godot3dToolkit;
[GlobalClass]
public partial class Pickup : InteractableResource
{
    [Export]
    public int Amount {get;set;} = 0;
    public override uint PhysicsLayer {
        get => 1 << 3 ;
        set { return;}
    }
}