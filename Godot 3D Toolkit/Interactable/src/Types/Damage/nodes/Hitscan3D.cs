

namespace Godot3dToolkit;
[GlobalClass]
public partial class Hitscan3D : InteractionRayCast3D<Damage>
{
    [Export]
    Damage damage {get;set;}

    [Export]
    bool fireFromInspector {
        get => false;
        set => Fire();
    }

    public override Damage Resource => damage;
    public override bool CloneResourceOnAccepted {get;init;} = true;

    protected override bool rayEnabled {get;set;} = false;

    [Export]
    public override float Range {
        get => base.Range;
        set => base.Range = value;
    }


public override void _Input(InputEvent @event)
{
    if (@event.IsActionPressed("Fire"))
    {
        Fire();
    }
}

    public override void HandleHit(Node target)
    {
        GD.Print(target);
    }
}