using System;

namespace Godot3dToolkit;

public interface IInteractor<T> where T : InteractableResource,  new()
{    

    Node ReplyNode {get;set;}

    void SetPhysicsLayers();

    void AcceptResource(T resource);
    virtual bool HandleHandshake(IInteractable<T> source){
        AcceptResource(source.CloneResourceOnAccepted ? source.UniqueResource : source.Resource);
        return true;
    }

}
