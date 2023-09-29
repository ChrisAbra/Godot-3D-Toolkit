using System;

namespace Godot3dToolkit;

public interface IInteractor<T> where T : Resource
{
    public bool Interact(T resource, IInteractable<T> source = null) ;

}
