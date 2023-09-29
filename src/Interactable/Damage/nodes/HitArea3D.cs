namespace Godot3dToolkit;
[GlobalClass]
public partial class HitArea3D : Area3D, IDamageable
{

    public bool Interact(Damage damage, IInteractable<Damage> source = null)
    {
        throw new NotImplementedException();
    }
}