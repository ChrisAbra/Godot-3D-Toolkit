using System;

namespace Godot3dToolkit;

public interface IInteractor<T> where T : Resource
{    

    Node ReplyNode {get;set;}
    virtual void AcceptResource(T resource){
        return;
    }
    virtual bool HandleHandshake(IInteractable<T> source){
        AcceptResource(source.duplicatedResource);
        return true;
    }

}
