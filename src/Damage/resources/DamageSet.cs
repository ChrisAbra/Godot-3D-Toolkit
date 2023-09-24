namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/DamageSet.svg")]
public partial class DamageSet : Resource
{
    [Export]
    public Array<Damage> Damages { get; set; } = new Array<Damage>{
        new Damage{Amount = 10, Type = Damage.DamageType.Impact}
    };

}