using Godot3dToolkit.InputManager;

namespace Godot3dToolkit;
[GlobalClass]
[Icon("uid://ds4rfwt5ys6uu")]
public partial class ThirdPersonPlayerController : PlayerController
{

    [Export]
    public override int PlayerIndex {get;set;} = 0;

    [MapToInput("Move")]
    public Vector2 MoveDirection;

    [MapToInput("Look")]
    public Vector2 LookDirection;
    
    [MapToInput("Sprint")]
    public bool SprintButtonPressed;

    [MapToInput("Aim")]
    public bool AimButtonPressed;

    //Hold examples
    [MapToInput("Crouch")]
    public bool CrouchingButtonPressed;

    //One-shot example
    [MapToInput("Shoot")]
    public bool ShootButtonPressed
    {
        set
        {
            GD.Print(value);
            if (value == true) EmitSignal(SignalName.Shoot);
        }
    }


    [Signal]
    public delegate void ShootEventHandler();


}