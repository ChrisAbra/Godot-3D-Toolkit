using System;

namespace Godot3dToolkit;
public abstract partial class InteractorArea3D<T> : Area3D, IInteractor<T> where T : InteractableResource,  new()
{
    public virtual Node ReplyNode {get;set;}

    public InteractorArea3D(){
        ReplyNode = this;
        SetPhysicsLayers();
    }

    protected abstract void AcceptResource(T resource);


    public void SetPhysicsLayers()
    {
        CollisionLayer = new T().PhysicsLayer;
        CollisionMask = 0;
    }

    void IInteractor<T>.AcceptResource(T resource)
    {
        AcceptResource(resource);
    }
}
