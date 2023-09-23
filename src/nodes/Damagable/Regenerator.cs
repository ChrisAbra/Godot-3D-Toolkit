namespace Godot3dToolkit;
[GlobalClass]
[Icon("res://src/nodes/Damagable/assets/Recharger.svg")]

public partial class Regenerator : Node
{

	[Export]
	public double Rate { get; set; } = 0f;

	[Export]
	public double Limit { get; set; } = 0f;
	private double usedRechargeAmount;
	private bool isLimited { get { return Limit > Mathf.Epsilon;} }

	[Export]
	public double Delay
	{
		get { return _delay; }
		set { _delay = value <= Mathf.Epsilon ? 0f : value; }
	}
	private double _delay;

	[Export]
	public double DelayOnDamage {get;set;}


	private IDamageable? parent;

	[Export]
	public bool RemoveWhenConsumed = false;


	public override void _Ready()
	{
		parent = GetParentOrNull<IDamageable>();
		if (parent is null) GD.PushError("The HealthRecharger Node's is not attached to a component which implements IDamageable, such as Health");
	}


	public override void _Process(double delta)
	{
		if (Engine.IsEditorHint()) return;
		if (parent is null) return;

		if (Delay > Mathf.Epsilon)
		{
			Delay -= delta;
			return;
		}

		if (Rate <= Mathf.Epsilon) return;
		if (parent.Amount >= parent.MaxAmount) return;

		if (usedRechargeAmount >= Limit)
		{
			GD.Print("Recharge complete");
			GD.Print(RemoveWhenConsumed);
			if (RemoveWhenConsumed) QueueFree();
			return;
		}

		double rechargeAmount = delta * Rate;

		parent.Amount += rechargeAmount;
		if(isLimited) usedRechargeAmount += rechargeAmount;

		GD.Print("Health:", parent.Amount);
	}

	public void TookDamage(){
		if(DelayOnDamage > 0){
			Delay = DelayOnDamage;
		}
	}
}
