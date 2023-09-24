namespace Godot3dToolkit;

[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Health.svg")]
public partial class Health : BaseHealth
{
    [Signal]
    public delegate void DiedEventHandler();


    public override void _Ready()
    {
        base._Ready();

        HealthResource.Empty += () =>
        {
            EmitSignal(SignalName.Died);
        };

    }

    public override void TakeDamage(DamageSet damageSet){
        //TODO: Add ability to forward damage to shield;
        base.TakeDamage(damageSet);
    }


}

