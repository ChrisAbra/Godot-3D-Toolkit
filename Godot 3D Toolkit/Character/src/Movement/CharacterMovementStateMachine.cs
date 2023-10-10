using System;
using Godot3dToolkit.StateMachines;

namespace Godot3dToolkit.Character;

[GlobalClass]
[Icon("uid://p22dsoj1fm83")]

public partial class CharacterMovementStateMachine : ResourceStateMachine<CharacterMovementStats>
{

    [Export]
    public override CharacterMovementStats DefaultResource {get;set;}

    [Export]
    public CharacterMovementStats AimingStats;
    public bool isAiming;
    [Export]
    public CharacterMovementStats SprintingStats;
    public bool isSprinting;
    public bool isGrounded;


    public override void _Process(double delta)
    {
        if(!isGrounded) ActiveResource = DefaultResource;

        else if(isAiming) ActiveResource = AimingStats;

        else if(isSprinting) ActiveResource = SprintingStats;

        else if(ActiveResource != DefaultResource) ActiveResource = DefaultResource;
    }

}   
