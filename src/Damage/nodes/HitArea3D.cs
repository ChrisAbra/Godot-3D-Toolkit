namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Damagable.svg")]
public partial class HitArea3D : Area3D, IHitbox
{
    [Export]
    public Damage.DamageType RecievesDamageTypes { get; set; } = Damage.DamageType.Impact;

    [Export]
    public DamageModiferSet Modifier { get; set; }

    [Export]
    public BaseHealth Health { get; set; }


    public HitArea3D()
    {
        Monitoring = false;
    }

    public void HandleDamage(DamageSet damage)
    {
        GD.Print("TakeDamage");
        GD.Print(damage);
        damage = (this as IHitbox).ApplyModifiers(ref damage);
        Health?.TakeDamage(ref damage);
    }

}