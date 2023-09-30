
namespace Godot3dToolkit;
[GlobalClass]
public partial class HurtArea3D : InteractionArea3D<Damage>
{
    [Export]
    public Damage Damage {get;set;}

    public override Damage duplicatedResource => (Damage)Damage.Duplicate(true);

    public override void InteractionAccepted(IInteractor<Damage> interactor, Node interactingNode)
    {
        throw new NotImplementedException();
    }
}