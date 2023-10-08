using System;
using Godot3dToolkit.StateMachines;

namespace Godot3dToolkit.Character;

[GlobalClass]
[Icon("uid://dqcqowp6gtfvh")]
public partial class CharacterMovementState : State<CharacterMovementStats>
{
    [Export]
    public override CharacterMovementStats Resource {get;set;}
    
    public override bool CheckIfActive()
    {
        throw new NotImplementedException();
    }
}
