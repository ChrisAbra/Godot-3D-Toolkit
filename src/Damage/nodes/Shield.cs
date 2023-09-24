namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Shield.svg")]
public partial class Shield : BaseHealth
{

    [Signal]
    public delegate void DestroyedEventHandler();
    [Export]
    public bool QueueFreeOnEmpty = false;

    [Export]
    public Damage.DamageType ProtectsFrom = Damage.DamageType.Impact;

    [Export]
    public bool TransferRemainingDamageToHealthWhenDestroyed = false;

    public DamageSet RemainingDamage;


    async public override void _Ready()
    {
        base._Ready();

        if (QueueFreeOnEmpty)
        {
            HealthResource.Empty += () =>
            {
                EmitSignal(SignalName.Destroyed);
                QueueFree();
            };
        }

    }


}