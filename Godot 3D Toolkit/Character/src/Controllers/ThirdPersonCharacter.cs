namespace Godot3dToolkit.Character;

[GlobalClass]

public partial class ThirdPersonCharacter : CharacterBase
{

    [Export]
    public SpringArm3D CameraArm;
    [Export]
    public VirtualCamera VirtualCamera;

    protected Vector2 lookVector;

    public override void _Input(InputEvent @event)
    {
        /*
        if(@event.IsActionPressed("AIM")) MovementStateMachine.isAiming = true;
        if(@event.IsActionReleased("AIM")) MovementStateMachine.isAiming = false;

        if(@event.IsActionPressed("SPRINT")) MovementStateMachine.isSprinting = true;
        else if(@event.IsActionReleased("SPRINT")) MovementStateMachine.isSprinting = false;
        */
    }

    protected bool IsCoyoteGrounded()
    {
        return IsOnFloor() ? true : UngroundedTime < movementStats.CoyoteTime;
    }

    public override void _Ready()
    {
        base._Ready();
        CameraArm ??= GetNode<SpringArm3D>("%CameraArm");
        VirtualCamera ??= GetNode<VirtualCamera>("%VirtualCamera");
        movementStats.HeightToRange ??= new Curve() { MinValue = 1, MaxValue = 1 };
    }



    public override void _PhysicsProcess(double delta)
    {
        deltaf = (float)delta;
        MovementStateMachine.isGrounded = IsOnFloor();

        CameraMove();
        //Zoom();
        PlayerMove();
        base._PhysicsProcess(delta);
    }

    public virtual void Zoom()
    {
        VirtualCamera.Tightness = movementStats.CameraPositionTightness;
        VirtualCamera.Attributes.FrustumFocalLength = Mathf.MoveToward(
            VirtualCamera.Attributes.FrustumFocalLength,
            movementStats.FocalLength
            , movementStats.ZoomSpeed * deltaf);
    }

    protected virtual void CameraMove()
    {

        var cameraPosition = CameraArm.Position;
        Vector2 lookDirectionInput = -Input.GetVector("LOOK_LEFT", "LOOK_RIGHT", "LOOK_UP", "LOOK_DOWN");

        if (lookDirectionInput != Vector2.Zero)
        {
            CameraArm.RotateY(lookDirectionInput.X * movementStats.LookSpeedHorizontal * deltaf);
            strafeTargetAngle = CameraArm.Rotation.Y;
            cameraPosition.Y += lookDirectionInput.Y * (movementStats.InvertY ? 1 : -1) * movementStats.LookSpeedVertical * deltaf;
        }
        else if (Velocity.Length() > 1)
        {
            cameraPosition.Y = Mathf.Lerp(cameraPosition.Y, movementStats.DefaultCameraHeight, movementStats.CameraRestitution * deltaf);
        }

        cameraPosition.Y = Mathf.Clamp(cameraPosition.Y, movementStats.MinCameraHeight, movementStats.MaxCameraHeight);

        if (movementStats?.HeightToRange is not null)
        {
            CameraArm.SpringLength = movementStats.CameraArmSpringLength(cameraPosition.Y);
        }

        CameraArm.Position = cameraPosition;

    }


    protected virtual void PlayerMove()
    {
        if (!IsCoyoteGrounded()) return;

        if (Input.IsActionJustPressed("JUMP")) Jump();


        var velocity = Velocity;

        Vector2 moveDirectionInput = Input.GetVector("MOVE_LEFT", "MOVE_RIGHT", "MOVE_FORWARD", "MOVE_BACKWARDS").Normalized();
        Vector3 direction = new Vector3(moveDirectionInput.X, 0, moveDirectionInput.Y).Rotated(Vector3.Up, CameraArm.Rotation.Y);

        if (direction != Vector3.Zero)
        {
            //Move
            velocity.X = Mathf.MoveToward(Velocity.X, direction.X * movementStats.Speed, movementStats.Acceleration); ;
            velocity.Z = Mathf.MoveToward(Velocity.Z, direction.Z * movementStats.Speed, movementStats.Acceleration); ;
        }
        else
        {
            //Stop
            velocity.X = Mathf.MoveToward(Velocity.X, 0, movementStats.Acceleration);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, movementStats.Acceleration);
        }

        Velocity = velocity;

    }


}
