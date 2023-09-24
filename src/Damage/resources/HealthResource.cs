namespace Godot3dToolkit;
[Tool]
[GlobalClass]
public partial class HealthResource : Resource
{

    [Signal]
    public delegate void DamagedEventHandler();
    [Signal]
    public delegate void EmptyEventHandler();

    [Export]
    public double Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            if (value < _amount)
            {
                EmitSignal(SignalName.Damaged);
            }
            if (value <= Mathf.Epsilon)
            {
                EmitSignal(SignalName.Empty);
                value = 0;
            }
            if (value > MaxAmount)
            {
                value = MaxAmount;
            }

            _amount = value;

        }
    }

    private double _amount;

    [Export]
    public double MaxAmount { get; set; } = 100;
}