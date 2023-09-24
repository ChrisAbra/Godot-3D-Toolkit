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

    [Export]
    public bool ShootNowInEditor
    {
        get => false;
        set => Fire();
    }
    public override void _Ready()
    {
        Enabled = false; //Fired Manually
        CollideWithAreas = true; //HitBoxes extend Area3D 
        TargetPosition = Vector3.Forward * Range; //Move/Orient Parent node such as end of gun or make child of camera (consider parallax issues)
    }

    public void Fire()
    {
        ForceRaycastUpdate();

        var target = GetCollider();

        if (target is not IDamageable damageable) return;

        var position = GetCollisionPoint();
        var normal = GetCollisionNormal();

        damageable.TakeDamage(Damage);

        EmitSignal(SignalName.SuccessfulHit, target);
    }

    public CollisionObject3D Test()
    {
        ForceRaycastUpdate();
        return GetCollider() is CollisionObject3D collisionObject ? collisionObject : null;
    }
}