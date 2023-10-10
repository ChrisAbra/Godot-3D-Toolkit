using System;

namespace Godot3dToolkit;

[Icon("uid://p22dsoj1fm83")]
public abstract partial class ResourceStateMachine<T> : Node  where T : Resource
{
    public abstract T DefaultResource {get;set;}
    public virtual T ActiveResource {get; protected set;}

    public override void _Ready()
    {
        ActiveResource = DefaultResource;
    }

}
