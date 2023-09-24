

namespace Godot3dToolkit;

[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Health.svg")]
public partial class Health : Node, IDamageable
{

    public double Amount {get;set;} 
    [Export]
    public double MaxAmount {get;set;} = 100;

    [Export]
    public Node DamagedReturnNode {get;set;}

    public Health() : base(){
        Amount = MaxAmount;
    }

    public Node TakeDamage(DamageSet damages)
    {
        foreach(var damage in damages.Damages){
            Amount -= damage.Amount;
        }
        return DamagedReturnNode ?? this;
    }
}