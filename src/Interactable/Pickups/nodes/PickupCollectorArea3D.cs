namespace Godot3dToolkit;
[GlobalClass]
public partial class PickupCollectorArea3D : InteractorArea3D<Pickup>
{
    [Signal]
    public delegate void PickupEventHandler(Pickup pickup, Node node);
    public override bool Interact(Pickup pickup, IInteractable<Pickup> source = null)
    {
        GD.Print(pickup.Amount);
        GD.Print(source);
        EmitSignal(SignalName.Pickup,pickup);
        return true;
    }
}