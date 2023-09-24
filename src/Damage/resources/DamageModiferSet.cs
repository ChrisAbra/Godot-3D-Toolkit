
namespace Godot3dToolkit;

[Tool]
[GlobalClass]
public partial class DamageModiferSet : Resource
{
    [Export]
    public Array<DamageModifier> Modifiers { get; set; } = new Array<DamageModifier>{
        new DamageModifier()
    };    

    public DamageSet ApplyModifiers(DamageSet damageSet){
        foreach(var modifier in Modifiers){
            damageSet = modifier.ApplyModifier(damageSet);
        }
        return damageSet;
    }

}
