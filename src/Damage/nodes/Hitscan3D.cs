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
    public float Knockback = 0f;

    [Export]
    public bool ShootNowInEditor
    {
        get => false;
        set
        {
            if (value) Fire();
        }
    }
    public override void _Ready()
    {
        Enabled = false; //Fired Manually
        CollideWithAreas = true; //HitBoxes extend Area3D 
        TargetPosition = Vector3.Forward * Range; //Move/Orient Parent node such as end of gun or make child of camera (consider parallax issues)
    }

    public void Fire()
    {
        if (Damage is null) { GD.PrintErr("Damage on Hitscan3D not set: ", this); return; }

        ForceRaycastUpdate();

        var target = GetCollider();

        if (target is not IHitbox hitbox) return;

        Hit hit = new Hit
        {
            damage = (DamageSet)Damage.Duplicate(true),
            impulse = GetCollisionPoint() * Knockback,
            hitNormal = GetCollisionNormal()
        };


        hitbox.TakeHit(hit);

        EmitSignal(SignalName.SuccessfulHit, target);
    }

    public CollisionObject3D Test()
    {
        ForceRaycastUpdate();
        return GetCollider() is CollisionObject3D collisionObject ? collisionObject : null;
    }
}