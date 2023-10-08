using Godot;
using System;

namespace Godot3dToolkit;

[GlobalClass]
public partial class VirtualCamera : Marker3D
{
	[Export]
	public CameraAttributesPhysical Attributes;
	[Export]
	public float Tightness = 10;


	StringName CameraGroup = "CameraPosition";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(CameraGroup);
		AddToGroup(CameraGroup);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
