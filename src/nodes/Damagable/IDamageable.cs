
namespace Godot3dToolkit;
public interface IDamageable
{

    public void OnDestroyed();
    public double Amount {get;set;}
    public double MaxAmount {get;set;}
    public void TakeDamage(Damage damage);

}
