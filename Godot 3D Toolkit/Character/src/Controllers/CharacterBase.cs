namespace Godot3dToolkit.Character;


[GlobalClass]
public partial class CharacterBase : CharacterBody3D
{

    [Export]
    protected Node3D CharacterModel;

    protected CharacterMovementStats movementStats { 
        get => MovementStateMachine?.ActiveState?.Resource ?? new();
    }

    [Export]
    public CharacterMovementStateMachine MovementStateMachine;


    protected float UngroundedTime = 0;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    protected float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    protected float deltaf;
    protected float strafeTargetAngle;

    private Vector3 previousVelocity = new();
    public Vector3 Acceleration = new();

    private bool validResource = false;

    protected float UngroundedMoveAttenuation
    {
        get => Mathf.Clamp(1 - UngroundedTime, 0, 1);
    }

    public override void _Ready()
    {
        base._Ready();
        CharacterModel ??= GetNode<Node3D>("%Character");
    }

    public override void _PhysicsProcess(double delta)
    {
        deltaf = (float)delta;
        calculateAcceleration();
        LocomoteWithVelocity();
    }

    private void calculateAcceleration(){
        Acceleration = (Velocity - previousVelocity) / deltaf;
        previousVelocity = Velocity;
    }

    public virtual void LocomoteWithVelocity()
    {
        
        Fall();
        MoveAndSlide();

        if (movementStats.CameraLock) RotateTowardStrafeTarget();

        if (Velocity.Length() > 0)
        {
            if ((Velocity * new Vector3(1, 0, 1)).Length() > 1)
            {
                if (!movementStats.CameraLock) RotateTowardVelocity();
            }
            CollideWithRigidBodies();
        }
    }

    public virtual void Fall()
    {

        if (!IsOnFloor())
        {
            UngroundedTime += deltaf;
            Velocity = Velocity with { Y = Velocity.Y - (gravity * deltaf) };
        }
        else
        {
            UngroundedTime = 0;
        }
    }

    public virtual void Jump()
    {
        if (IsOnFloor()) Velocity = Velocity with { Y = movementStats.JumpVelocity };
    }

    public virtual void RotateTowardStrafeTarget()
    {
        CharacterModel.Rotation = CharacterModel.Rotation with
        {
            Y = Mathf.LerpAngle(CharacterModel.Rotation.Y, strafeTargetAngle, movementStats.TurnSpeed * deltaf)
        }; ;

    }

    public virtual void RotateTowardVelocity()
    {
        //Cache rotation, look at new place, lerp between new and old, set to cached
        var modelRotation = CharacterModel.Rotation;
        CharacterModel.LookAt(Transform.Origin + Velocity, Vector3.Up);
        modelRotation.Y = Mathf.LerpAngle(modelRotation.Y, CharacterModel.Rotation.Y, movementStats.TurnSpeed * deltaf);
        CharacterModel.Rotation = modelRotation;
    }

    public virtual void CollideWithRigidBodies()
    {

        var pushImpulseStrength = movementStats.PushForce * Velocity.Length();
        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collider = GetSlideCollision(i);

            if (collider.GetCollider() is not RigidBody3D rigidBody) continue;

            rigidBody.ApplyCentralImpulse(-collider.GetNormal() * pushImpulseStrength);
        }
    }
}
