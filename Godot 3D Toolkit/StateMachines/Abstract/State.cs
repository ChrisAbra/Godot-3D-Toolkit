using System;

namespace Godot3dToolkit.StateMachines;

public abstract partial class State<T>: Node where T : Resource
{
    public delegate void StateEnteredEventHandler();
    public abstract T Resource {get;set;}

    public abstract bool CheckIfActive();
    public virtual void Tick(float deltaf){
        return;
    }
    public virtual void PhysicsTick(float deltaf){
        return;
    }
}
