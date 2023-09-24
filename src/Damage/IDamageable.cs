namespace Godot3dToolkit;
public partial interface IDamageable
{
	double Amount { get; set; }
	double MaxAmount { get; set; }

	public Node TakeDamage(DamageSet damage);
}