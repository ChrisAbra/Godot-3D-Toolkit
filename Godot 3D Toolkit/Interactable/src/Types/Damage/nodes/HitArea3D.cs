namespace Godot3dToolkit;
[GlobalClass]
public partial class HitArea3D : InteractorArea3D<Damage>, IDamageable, IRayHitable
{
    [Export]
    public override Node ReplyNode { get; set; }
    [Export]
    public RigidBody3D HitBody { get; set; }

    [Export]
    public float ImpulseMultipler {get;set;} = 1;

    public void TakeHit(IRayHitable.Hit hit)
    {
        HitBody?.ApplyImpulse((hit.position - hit.source) * ImpulseMultipler);
    }

    protected override void AcceptResource(Damage recievedDamage)
    {
        GD.Print(recievedDamage);
    }
}