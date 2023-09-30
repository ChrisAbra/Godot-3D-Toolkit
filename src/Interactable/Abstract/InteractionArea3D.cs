namespace Godot3dToolkit;
public abstract partial class InteractionArea3D<T> : Area3D, IInteractable<T> where T : Resource
{

    public abstract T duplicatedResource {get;}

    public InteractionArea3D(){
        Monitorable = false;
        ConnectHandshakeToEntered();
    }

    public virtual void ConnectHandshakeToEntered(){
        AreaEntered += (area) => {
            if(area is not IInteractor<T> interactor) return;
            (this as IInteractable<T>).Handshake(interactor);
        };
        BodyEntered += (node) => {
            if(node is not IInteractor<T> interactor) return;
            (this as IInteractable<T>).Handshake(interactor);
        };
    }

    public abstract void InteractionAccepted(IInteractor<T> interactor, Node interactingNode);

    public virtual void InteractionRejected(IInteractor<T> interactor, Node interactingNode)
    {
        return;
    }

}