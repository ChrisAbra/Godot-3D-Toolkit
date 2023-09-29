namespace Godot3dToolkit;
[GlobalClass]
public partial class Hitscan3D : RayCast3D, IDamaging
{
    [Export]
    public Damage Damage {get;set;}

    public Damage duplicatedResource => (Damage)Damage.Duplicate(true);

    public void Fire(){

        ForceRaycastUpdate();

        var target = GetCollider();
        if(target is not IDamageable damagable) return;

        damagable.TakeDamage(duplicatedResource, this);
    }

}