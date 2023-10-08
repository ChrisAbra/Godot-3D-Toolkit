using Godot;
using System;

namespace Godot3dToolkit;

[GlobalClass]
public partial class VirtualCameraBrain : Camera3D
{

	[Export]
	public float PositionTightness = 1;

	Array<Node> CameraPositions;
	Array<Node> CameraLookAt;
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
			if(activeCamera is not VirtualCamera cam) return;
			GlobalPosition = GlobalPosition.Lerp(cam.GlobalPosition,PositionTightness * (float)delta) ;
			Attributes = cam.Attributes;
			PositionTightness = cam.Tightness;

		}
		if(CameraLookAt.Count == 1){
			Node activeCamera = CameraLookAt[0];
			if(activeCamera is not VirtualCameraLook cam) return;
			LookAt(cam.GlobalPosition);
			HOffset = cam.HOffset;
			VOffset = cam.VOffset;
		}

	}
}
