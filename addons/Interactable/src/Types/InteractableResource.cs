using System;

namespace Godot3dToolkit;

public abstract partial class InteractableResource : Resource
{
    public abstract uint PhysicsLayer {get;set;}

    public InteractableResource(){}
}
