namespace Godot3dToolkit;


[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/Damage.svg")]
public partial class Damage : Resource
{
    [Flags]
    public enum DamageType
    {
        Impact = 1 << 0,
        Fire = 1 << 1,
    }
    
    [Export]
    public double Amount { get; set; }

    [Export]
    public DamageType Type   
    {
        get => _damageType;
        set {
            _damageType = _damageType ^ value; // Ensures only one value
        }
    }

    DamageType _damageType;

}