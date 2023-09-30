namespace Godot3dToolkit;
[GlobalClass]
public partial class HitArea3D : InteractorArea3D<Damage>, IDamageable
{
    [Export]
    public override Node ReplyNode { get; set; }

    protected override void AcceptResource(Damage recievedDamage)
    {
        GD.Print(recievedDamage);
    }
}