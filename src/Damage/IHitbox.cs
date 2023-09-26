namespace Godot3dToolkit;

public interface IHitbox
{
    [Export]
    public Damage.DamageType RecievesDamageTypes { get; set; }

    [Export]
    public DamageModiferSet Modifier { get; set; }

    [Export]
    public BaseHealth HealthNode { get; set; }

    public void TakeHit(Hit hit){
        GD.Print(hit);
        TakeDamage(hit.damage);
    }

    public void TakeDamage(DamageSet damage)
    {
        damage = (DamageSet)damage.Duplicate();
        damage = RemoveIncompatibleTypes(damage);
        damage = ApplyModifiers(damage);
        HealthNode?.TakeDamage(damage);
    }

    public DamageSet RemoveIncompatibleTypes(DamageSet damage){
        return damage;
    }

    public DamageSet ApplyModifiers(DamageSet damage)
    {
        if (Modifier is not null)
        {
            damage = Modifier.ApplyModifiers(damage);
        }
        return damage;

    }

}
