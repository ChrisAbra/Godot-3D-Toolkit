using System;

namespace Godot3dToolkit;
public abstract partial class InteractorArea3D<T> : Area3D, IInteractor<T> where T : Resource
{
    public virtual Node ReplyNode {get;set;}

    public InteractorArea3D(){
        ReplyNode = this;
    }

    protected abstract void AcceptResource(T resource);

    public bool HandleHandshake(IInteractable<T> source)
    {
        AcceptResource(source.duplicatedResource);
        return true;
    }

}
