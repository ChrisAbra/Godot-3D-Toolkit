namespace Godot3dToolkit;

public struct Hit {
	public DamageSet damage;
	public Vector3 impulse;
	public Vector3 hitNormal;
}

public interface IDamageable
{
	public void TakeDamage(ref DamageSet damage);

	public void TakeDamage(ref Hit hit){
		TakeDamage(ref hit.damage);
	}

}