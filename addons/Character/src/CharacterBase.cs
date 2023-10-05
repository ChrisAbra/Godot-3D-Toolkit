using Godot;
using System;
namespace Godot3dToolkit;


[GlobalClass]
public partial class CharacterBase : CharacterBody3D
{
    [Export]
    public float Speed = 5.0f;
    [Export]
    public float JumpVelocity = 4.5f;

    [Export]
    public bool CameraLock = false;

    [Export]
    public float TurnSpeed = 4.5f;

    [Export]
    public float PushForce = 1;

    protected float UngroundedTime = 0;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    protected float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    [Export]
    protected Node3D CharacterModel;

    protected float deltaf;
	protected float strafeTargetAngle;

    private Vector3 VECTOR3_UP = Vector3.Up;

    protected Vector2 moveVector;


    public override void _Ready()
    {
        base._Ready();
        CharacterModel ??= GetNode<Node3D>("%Character");
    }


    public override void _PhysicsProcess(double delta)
    {
        deltaf = (float)delta;
        LocomoteWithVelocity();
    }

    public virtual void LocomoteWithVelocity(){

        Fall();
		MoveAndSlide();

        if (Velocity.Length() > 0)
        {
            if((Velocity * new Vector3(1,0,1)).Length() > 1){
                if (!CameraLock) RotateTowardVelocity();
                else RotateTowardStrafeTarget();
            }
            CollideWithRigidBodies();
        }
    }

    public virtual void Fall(){

        if (!IsOnFloor())
		{
            UngroundedTime += deltaf;
			Velocity = Velocity with {Y = Velocity.Y - (gravity * deltaf)};
		}
        else{
            UngroundedTime = 0;
        }
    }

    public virtual void Jump(){
        if (IsOnFloor()) Velocity = Velocity with {Y = JumpVelocity};
    }

    public virtual void RotateTowardStrafeTarget(){
        CharacterModel.Rotation = CharacterModel.Rotation with {
            Y = Mathf.LerpAngle(CharacterModel.Rotation.Y, strafeTargetAngle, TurnSpeed * deltaf)
        };;

    }

    public virtual void RotateTowardVelocity()
    {
        //Cache rotation, look at new place, lerp between new and old, set to cached
        var modelRotation = CharacterModel.Rotation;
        CharacterModel.LookAt(Transform.Origin + Velocity, VECTOR3_UP);
        modelRotation.Y = Mathf.LerpAngle(modelRotation.Y, CharacterModel.Rotation.Y, TurnSpeed * deltaf);
        CharacterModel.Rotation = modelRotation;
    }

    public virtual void CollideWithRigidBodies()
    {

        var pushImpulseStrength = PushForce * Velocity.Length();
        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collider = GetSlideCollision(i);
            
            if (collider.GetCollider() is not RigidBody3D rigidBody) continue;

            rigidBody.ApplyCentralImpulse(-collider.GetNormal() * pushImpulseStrength);
        }
    }
}
