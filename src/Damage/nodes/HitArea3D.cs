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
    public BaseHealth HealthNode { get; set; }


    public HitArea3D()
    {
        Monitoring = false;
    }

}