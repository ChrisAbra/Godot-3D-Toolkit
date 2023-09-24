using System.ComponentModel;

namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Hitscan3D.svg")]
public partial class Hitscan3D : RayCast3D, IDamageCausing
{

    [Signal]
    public delegate void SuccessfulHitEventHandler(Node target);

    [Export]
    public DamageSet Damage { get; set; }

    [DefaultValue(10f)]
    [Export]
    public float Range
    {
        get { return _range; }
        set
        {
            _range = value;
            TargetPosition = Vector3.Forward * value;
        }
    }
    public float _range;

    public override void _Ready()
    {
        Enabled = false;
        CollideWithAreas = true;
        CollideWithBodies = false;
        TargetPosition = Vector3.Forward * Range;
    }

    public void Fire()
    {
        if (Test() is not IDamageable damageable) return;

        var target = damageable.TakeDamage(Damage);

        EmitSignal(SignalName.SuccessfulHit, target);
    }

    public CollisionObject3D Test()
    {
        ForceRaycastUpdate();
        
        return GetCollider() is CollisionObject3D collisionObject ? collisionObject : null;
    }
}