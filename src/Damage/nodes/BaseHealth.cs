namespace Godot3dToolkit;
[Tool]
[GlobalClass]
public abstract partial class BaseHealth : Node, IDamageable
{


    public double Amount
    {
        get => HealthResource.Amount;
        set
        {
            HealthResource.Amount = value;
        }
    }

    public double MaxAmount {get;set;}

    public override void _Ready()
    {
        Amount = HealthResource.Amount;
        MaxAmount = HealthResource.MaxAmount;
    }

    [Export(PropertyHint.Range)]
    public HealthResource HealthResource = new();

    public virtual void TakeDamage(DamageSet damageSet)
    {
        foreach(var damage in damageSet.Damages){
            Amount -= damage.Amount;
        }
    }
    
}