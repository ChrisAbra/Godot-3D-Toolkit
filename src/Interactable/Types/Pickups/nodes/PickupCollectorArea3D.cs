namespace Godot3dToolkit;
[GlobalClass]
public partial class PickupCollectorArea3D : InteractorArea3D<Pickup>
{
    [Signal]
    public delegate void PickupEventHandler(Pickup pickup, Node node);

    protected override void AcceptResource(Pickup pickup)
    {
        GD.Print(pickup.Amount);
        EmitSignal(SignalName.Pickup,pickup);
    }
}