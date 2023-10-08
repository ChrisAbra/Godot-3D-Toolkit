using System;

namespace Godot3dToolkit;

[GlobalClass]
public partial class CharacterMovementStats : Resource
{

    [ExportGroup("Basic")]

    [Export]
    public float Speed = 5.0f;
    [Export]
    public float Acceleration = 1f;

    [Export]
    public float JumpVelocity = 4.5f;

    [Export]
    public bool CameraLock = false;

    [Export]
    public float TurnSpeed = 4.5f;

    [Export]
    public float PushForce = 1;

    [ExportGroup("Third-Person Controller")]

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
    public float CameraRestitution = 0.5f;
    [Export]
    public Curve HeightToRange;
    [Export]
    public float CoyoteTime = 0.2f;

    [Export]
    public float FocalLength = 15; 
    [Export]
    public float CameraPositionTightness = 10; 

    [Export]
    public float ZoomSpeed = 15; 

    public virtual float CameraArmSpringLength(float cameraPositionY){
        return HeightToRange.Sample(
            Mathf.InverseLerp(MinCameraHeight, MaxCameraHeight, cameraPositionY));
    }



}
