using System;

namespace Godot3dToolkit;
[Tool]
[GlobalClass]
public partial class DamageModifier : Resource
{

    public enum ModiferType{

        Attenuate,
        Multiply,

    }

    
    [Export]
    public ModiferType Type {get;set;}

    [Export]

    public double Amount {get;set;}
    [Export]

    public Damage.DamageType DamageType 
    {
        get => _damageType;
        set {
            _damageType = _damageType ^ value; // Ensures only one value
        }
    }

    Damage.DamageType _damageType; 


    public DamageSet ApplyModifier(DamageSet damageSet){
        GD.Print("Damage Modifiers not implemented");
        //TODO: Implement DamageModifiers
        return damageSet;
    }

}
