namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Damagable.svg")]
public partial class HitBox3D : Area3D
{
    [Export]
    public Damage.DamageType RecievesDamageTypes = Damage.DamageType.Impact;

    [Export]
    public DamageSet Multipler {get;set;}

    [Export]
    public Health Health {get;set;}



    public HitBox3D(){
        Monitoring = false;
    }

    public void TakeDamage(DamageSet damage){
        //if(damage.Types != RecievesDamageTypes) return;

        Health.TakeDamage(damage);
    }

}