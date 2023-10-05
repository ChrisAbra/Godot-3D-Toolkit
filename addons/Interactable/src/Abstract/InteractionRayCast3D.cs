using System;

namespace Godot3dToolkit;
[Icon("res://src/Interactable/Types/Damage/assets/Hitscan3D.svg")]
public abstract partial class InteractionRayCast3D<T> : RayCast3D, IInteractable<T> where T : InteractableResource, new()
{

    public abstract T Resource { get; }
    protected abstract bool rayEnabled { get; set; }

    public virtual float Range
    {
        get => TargetPosition.Z * -1;
        set => TargetPosition = Vector3.Forward * value;
    }
    public virtual bool CloneResourceOnAccepted { get; init; } = false;

    public abstract void HandleHit(Node target);
    public virtual void Miss()
    {
        return;
    }

    public InteractionRayCast3D()
    {
        Enabled = rayEnabled;
        TargetPosition = Vector3.Forward * Range;
        InitPhysicsLayers();
    }

    public virtual void Fire()
    {
        GD.Print("Fire!");
        ForceRaycastUpdate();
        Test(GetCollider());
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Engine.IsEditorHint()) return;
        if (!rayEnabled) return;
        base._PhysicsProcess(delta);
        Test(GetCollider());
    }

    public void Test(GodotObject target)
    {
        if (target is not IInteractor<T> interactable)
        {
            Miss();
            return;
        }

        if (target is IRayHitable hittable)
        {
            hittable.TakeHit(new IRayHitable.Hit
            {
                target = target as Node,
                position = GetCollisionPoint(),
                normal = GetCollisionNormal(),
                source = GlobalPosition

            });
        }

        (this as IInteractable<T>).Handshake(interactable);

    }

    public void InteractionAccepted(IInteractor<T> interactor, Node interactingNode)
    {
        HandleHit(interactingNode);
    }

    public virtual void InteractionRejected(IInteractor<T> interactor, Node interactingNode)
    {
        return;
    }

    public void InitPhysicsLayers()
    {
        CollisionMask = new T().PhysicsLayer;
        CollideWithAreas = true;
        CollideWithBodies = true;
    }
}
