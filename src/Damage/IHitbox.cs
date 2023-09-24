using System;

namespace Godot3dToolkit;

public interface IHitbox
{
    [Export]
    public Damage.DamageType RecievesDamageTypes { get; set; }

    [Export]
    public DamageModiferSet Modifier { get; set; }

    [Export]
    public BaseHealth Health { get; set; }

    public DamageSet ApplyModifiers(DamageSet damage){
        if (Modifier is not null)
        {
            damage = Modifier.ApplyModifiers(damage);
        }
        return damage;

    }

}
