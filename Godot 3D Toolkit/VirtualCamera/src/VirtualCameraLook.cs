using Godot;
using System;

namespace Godot3dToolkit;

[GlobalClass]
public partial class VirtualCameraLook : Marker3D
{

	StringName CameraGroup = "CameraLookAt";

	[Export]
	public float HOffset = 0f;
	[Export]
	public float VOffset = 0f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup(CameraGroup);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
