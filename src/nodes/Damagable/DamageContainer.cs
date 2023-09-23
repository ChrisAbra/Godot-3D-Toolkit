namespace Godot3dToolkit;

[GlobalClass]
[Icon("res://src/nodes/Damagable/assets/Damagable.svg")]

public partial class DamageContainer : Node, IDamageable
{
	[Signal]
	public delegate void DestroyedEventHandler();
	[Signal]
	public delegate void HealthChangedEventHandler(double newAmount, bool damaged, double ratioOfAmount, double ratioOfMax);

	[Signal]
	public delegate void UnhandledDamageEventHandler(Damage damage, double amountHandled);

	[Export]
	public double Amount
	{
		get { return _amount; }
		set
		{
			EmitSignal(SignalName.HealthChanged, _amount, value < _amount, value / _amount, value / MaxAmount);
			if (value > MaxAmount)
			{
				_amount = MaxAmount;
				return;
			}
			if (value <= Mathf.Epsilon)
			{
				_amount = 0f;
				OnDestroyed();
				return;
			}

		} 
	}
	protected double _amount;
    [Export]
    public double MaxAmount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	private bool isDestroyed = false;

    private Array<DamageContainer> subComponents = new();


    public void RegisterSubComponent(DamageContainer node){
        subComponents.Add(node);
    }

    public void OnDestroyed()
	{
		isDestroyed = true;
		EmitSignal(SignalName.Destroyed);
	}

	public void TakeDamage(Damage damage)
	{
		if (isDestroyed) return;
        
        foreach(var subComponent in subComponents){
            if(subComponent.isDestroyed) continue;
            subComponent.TakeDamage(damage); 
            return;
        }
 
		double unhandledDamage = 0f;
		if (Amount < damage.Amount)
		{
			unhandledDamage = damage.Amount - Amount;
		}
		Amount -= damage.Amount;
		
		if (unhandledDamage > Mathf.Epsilon)
		{
			EmitSignal(SignalName.UnhandledDamage, damage, unhandledDamage);
		}
	}
}
