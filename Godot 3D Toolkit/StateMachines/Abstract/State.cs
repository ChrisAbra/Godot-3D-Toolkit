using System;

namespace Godot3dToolkit.StateMachines;

public abstract partial class State<T,U>: Node where T : Node where U : StateMachine<T>
{
    public delegate void StateEnteredEventHandler();
    public abstract Array<State<T,U>> AllowedTransitions {get;set;}
    protected U stateMachine;

    protected T target {get => stateMachine.Target;}


    public abstract bool CheckIfActive();
    public virtual void Tick(float deltaf){
        return;
    }
    public virtual void PhysicsTick(float deltaf){
        return;
    }
}
