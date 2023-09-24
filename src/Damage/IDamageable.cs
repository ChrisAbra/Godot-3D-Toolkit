namespace Godot3dToolkit;

public struct Hit {
	public DamageSet damage;
	public Vector3 impulse;
	public Vector3 hitNormal;
}

public interface IDamageable
{
	protected void TakeDamage(DamageSet damage);

	public void TakeDamage(Hit hit){
		TakeDamage(hit.damage);
	}

}