namespace Godot3dToolkit;
[GlobalClass]
[Icon("res://src/nodes/Damagable/assets/Health.svg")]
public partial class Health : DamageContainer
{

	private Regenerator? _recharger;


	private bool isDestroyed = false;

	public override void _Ready()
	{
		_recharger = GetChildOrNull<Regenerator>(0);
		var parent = GetParent<DamageContainer>();
		parent.RegisterSubComponent(this);

	}

	//
	public new void TakeDamage(Damage damage)
	{
		base.TakeDamage(damage);
		_recharger?.TookDamage();
	}


}
