namespace Godot3dToolkit;

[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Health.svg")]
public partial class Health : BaseHealth
{
    [Signal]
    public delegate void DiedEventHandler();
    async public override void _Ready()
    {
        base._Ready();

        HealthResource.Empty += () =>
        {
            EmitSignal(SignalName.Died);
        };

    }

    public override void TakeDamage(DamageSet damageSet){
        base.TakeDamage(damageSet);
    }


}

