

namespace Godot3dToolkit;
[GlobalClass]
public partial class Hitscan3D : InteractionRayCast3D<Damage>
{

    [Export]
    public Damage Damage {get;set;}

    [Export]
    public bool FireFromInspector {
        get => false;
        set => Fire();
    }

    public override Damage duplicatedResource => (Damage)Damage.Duplicate(true);

    protected override bool RayEnabled {get;set;} = false;

    [Export]
    public override float Range {
        get => base.Range;
        set => base.Range = value;
    }

    public override void Hit(Node target)
    {
        GD.Print("hit target: ", target);
    }

}