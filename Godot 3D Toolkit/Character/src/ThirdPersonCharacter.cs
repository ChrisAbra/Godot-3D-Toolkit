using System;
using Godot;

namespace Godot3dToolkit.Character;

[GlobalClass]

public partial class ThirdPersonCharacter : CharacterBase
{

    [Export]
    public bool InvertY = false;
    [Export]
    public float LookSpeedVertical = 6;
    [Export]
    public float LookSpeedHorizontal = 5;
    [Export]
    public float MaxCameraHeight = 4;
    [Export]
    public float MinCameraHeight = 0.5f;
    [Export]
    public float DefaultCameraHeight = 2;
    [Export]
    public float DefaultCameraHeightRestitutionStrength = 0.5f;
    [Export]
    public Curve HeightToRange;

    [Export]
    public float CoyoteTime = 0.2f;
    [Export]
    public RemoteTransform3D CameraLockLookTarget;
    [Export]
    public RemoteTransform3D FreeLookTarget;


    [Export]
    public SpringArm3D CameraArm;
    protected Vector2 lookVector;


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if(@event.IsActionPressed("AIM")) Aim(true);
        else if(@event.IsActionReleased("AIM")) Aim(false);
    }

    protected bool IsCoyoteGrounded()
    {
        return IsOnFloor() ? true : UngroundedTime < CoyoteTime;
    }

    public override void _Ready()
    {
        base._Ready();
        CameraArm ??= GetNode<SpringArm3D>("%CameraArm");
        HeightToRange ??= new Curve() { MinValue = 1, MaxValue = 1 };

    }

    public override void _PhysicsProcess(double delta)
    {
        deltaf = (float)delta;
        CameraMove();
        PlayerMove();
        base._PhysicsProcess(delta);
    }

    private void Aim(bool enable){

        if(enable){
            CameraLock = true;
            CameraLockLookTarget.ProcessMode = ProcessModeEnum.Inherit;
            FreeLookTarget.ProcessMode = ProcessModeEnum.Disabled;
        }
        else{
            CameraLock = false;
            CameraLockLookTarget.ProcessMode = ProcessModeEnum.Disabled;
            FreeLookTarget.ProcessMode = ProcessModeEnum.Inherit;
        }
    }

    protected virtual void CameraMove()
    {

        var cameraPosition = CameraArm.Position;
        Vector2 lookDirectionInput = -Input.GetVector("LOOK_LEFT", "LOOK_RIGHT", "LOOK_UP", "LOOK_DOWN");

        if (lookDirectionInput != Vector2.Zero)
        {
            CameraArm.RotateY(lookDirectionInput.X * LookSpeedHorizontal * deltaf);
            strafeTargetAngle = CameraArm.Rotation.Y;
            cameraPosition.Y += lookDirectionInput.Y * (InvertY ? 1 : -1) * LookSpeedVertical * deltaf;
        }
        else if (Velocity.Length() > 1)
        {
            cameraPosition.Y = Mathf.Lerp(cameraPosition.Y, DefaultCameraHeight, DefaultCameraHeightRestitutionStrength * deltaf);
        }

        if (HeightToRange is not null)
        {
            CameraArm.SpringLength = HeightToRange.Sample(Mathf.InverseLerp(MinCameraHeight, MaxCameraHeight, CameraArm.Position.Y));
        }

        cameraPosition.Y = Mathf.Clamp(cameraPosition.Y, MinCameraHeight, MaxCameraHeight);
        CameraArm.Position = cameraPosition;

    }

    protected virtual void PlayerMove()
    {
        if(!IsCoyoteGrounded()) return;

        if (Input.IsActionJustPressed("JUMP") && IsCoyoteGrounded())
        {
            Jump();
        }


        var velocity = Velocity;

        Vector2 moveDirectionInput = Input.GetVector("MOVE_LEFT", "MOVE_RIGHT", "MOVE_FORWARD", "MOVE_BACKWARDS").Normalized();
        Vector3 direction = new Vector3(moveDirectionInput.X, 0, moveDirectionInput.Y).Rotated(Vector3.Up, CameraArm.Rotation.Y);

        if (direction != Vector3.Zero)
        {
            //Move
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            //Stop
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }


        Velocity = velocity;

    }


}
