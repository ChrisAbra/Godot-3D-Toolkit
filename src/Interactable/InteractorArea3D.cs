using System;

namespace Godot3dToolkit;
public abstract partial class InteractorArea3D<T> : Area3D, IInteractor<T> where T : Resource
{
    public abstract bool Interact(T resource, IInteractable<T> source = null);

}
