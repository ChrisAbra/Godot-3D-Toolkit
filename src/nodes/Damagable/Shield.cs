namespace Godot3dToolkit;
[GlobalClass]
[Icon("res://src/nodes/Damagable/assets/Shield.svg")]
public partial class Shield : Health
{	
	[Export]
	public bool RemoveWhenZero = false;
	public new void OnDestroyed()
	{
		base.OnDestroyed();
		if (RemoveWhenZero) QueueFree();
	}

}
