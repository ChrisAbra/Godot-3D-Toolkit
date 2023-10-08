using System;
using Godot3dToolkit.StateMachines;

namespace Godot3dToolkit.Character;

[GlobalClass]
[Icon("uid://p22dsoj1fm83")]

public partial class CharacterMovementStateMachine : StateMachine<CharacterMovementStats, CharacterMovementState>
{
    [Export]
    public override CharacterMovementState InitialState {get;set;}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if(@event.IsActionPressed("AIM")) Aim(true);
        else if(@event.IsActionReleased("AIM")) Aim(false);

        if(@event.IsActionPressed("SPRINT")) Sprint(true);
        else if(@event.IsActionReleased("SPRINT")) Sprint(false);

    }

    public void Aim(bool enable){
        if(enable){
            var aimState = GetNodeOrNull<CharacterMovementState>("%Aiming");
            TrySetState(aimState);
        }
        else{
            TrySetState(InitialState);
        }
    }

    public void Sprint(bool enable){
        if(enable){
            var sprintState = GetNodeOrNull<CharacterMovementState>("%Sprint");
            TrySetState(sprintState);
        }
        else{
            TrySetState(InitialState);
        }

    }



}   
