using System;

namespace Godot3dToolkit;
[Icon("res://src/Interactable/Types/Damage/assets/Hitscan3D.svg")]
public abstract partial class InteractionRayCast3D<T> : RayCast3D, IInteractable<T> where T : Resource
{
    public abstract T duplicatedResource {get;}
    protected abstract bool RayEnabled {get;set;}

    public virtual float Range {
        get => TargetPosition.Z * -1;
        set => TargetPosition = Vector3.Forward * value;
    }

    public abstract void Hit(Node target);
    public virtual void Miss(){
        return;
    }

    public InteractionRayCast3D(){
        Enabled = RayEnabled;
        TargetPosition = Vector3.Forward * Range;
        
    }

    public virtual void Fire(){
        GD.Print("Fire!");
        ForceRaycastUpdate();

        var target = GetCollider();
        GD.Print(target);
        if(target is not IInteractor<T> interactable) 
        {
            Miss();
            return;
        }

        (this as IInteractable<T>).Handshake(interactable);

    }

    public void InteractionAccepted(IInteractor<T> interactor, Node interactingNode)
    {
        Hit(interactingNode);
    }

    public virtual void InteractionRejected(IInteractor<T> interactor, Node interactingNode)
    {
        return;
    }
}
