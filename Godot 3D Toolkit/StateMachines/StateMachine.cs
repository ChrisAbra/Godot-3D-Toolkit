using System;
using Godot;
namespace Godot3dToolkit.StateMachines;


[Icon("uid://p22dsoj1fm83")]

public abstract partial class StateMachine<T> : Node where T : Node
{
    [Signal]
    public delegate void StateChangedEventHandler(State<T,StateMachine<T>> oldState, State<T,StateMachine<T>> newState);

    public abstract T Target {get;set;}

    public List<State<T,StateMachine<T>>> States;

    public abstract State<T,StateMachine<T>> InitialState {get;set;}
    public virtual State<T,StateMachine<T>> ActiveState {get;set;}

    public bool TrySetState(State<T,StateMachine<T>> newState){
        if(ActiveState == newState) return true;
        if(!ActiveState.AllowedTransitions.Contains(newState)) return false;

        EmitSignal(SignalName.StateChanged,ActiveState,newState);

        ActiveState = newState;
        return true;
    }

}
