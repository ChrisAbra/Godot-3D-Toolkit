using Godot;
using System;

namespace Godot3dToolkit;

[GlobalClass]
public partial class VirtualCameraBrain : CharacterBody3D
{

	[Export]
	public float Speed = 1;
	Godot.Collections.Array<Node> CameraPositions;
	Godot.Collections.Array<Node> CameraLookAt;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CameraPositions = GetTree().GetNodesInGroup("CameraPosition");
		CameraLookAt = GetTree().GetNodesInGroup("CameraLookAt");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(CameraPositions.Count == 1){
			Node activeCamera = CameraPositions[0];
			if(activeCamera is not Node3D cam) return;
			Velocity = cam.GlobalPosition -  GlobalPosition;
			Velocity *= cam.GlobalPosition.DistanceSquaredTo(GlobalPosition) * Speed;
			MoveAndSlide();
		}
		if(CameraLookAt.Count == 1){
			Node activeCamera = CameraLookAt[0];
			if(activeCamera is not Node3D cam) return;
			LookAt(cam.GlobalPosition);
		}

	}
}
