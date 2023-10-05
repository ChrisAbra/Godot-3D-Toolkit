
namespace Godot3dToolkit;
[GlobalClass]
public partial class HurtArea3D : InteractionArea3D<Damage>
{
    [Export]
    Damage damage {get;set;}

    public override Damage Resource => damage;

    public override bool CloneResourceOnAccepted {get;init;} = true;

    public override void InteractionAccepted(IInteractor<Damage> interactor, Node interactingNode)
    {
        throw new NotImplementedException();
    }
}