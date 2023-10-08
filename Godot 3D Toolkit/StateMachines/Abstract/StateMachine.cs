using System;
using Godot;
namespace Godot3dToolkit.StateMachines;


[Icon("uid://p22dsoj1fm83")]

public abstract partial class StateMachine<T, U> : Node where T : Resource where U : State<T>
{
    [Signal]
    public delegate void StateChangedEventHandler(State<T> oldState, State<T> newState);

    public List<U> States;

    public abstract U InitialState { get; set; }
    public virtual U ActiveState { get; set; }

    public override void _Ready(){
        ActiveState = InitialState;
    }

    public bool TrySetState(U newState)
    {
        if (ActiveState == newState) return true;

        EmitSignal(SignalName.StateChanged, ActiveState, newState);

        ActiveState = newState;
        return true;
    }

}
