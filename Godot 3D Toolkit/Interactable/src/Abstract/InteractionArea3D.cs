namespace Godot3dToolkit;
public abstract partial class InteractionArea3D<T> : Area3D, IInteractable<T> where T : InteractableResource,  new()
{

    public abstract T Resource {get;}
    public virtual bool CloneResourceOnAccepted {get;init;} = false;

    public InteractionArea3D(){
        Monitorable = false;
        ConnectHandshakeToEntered();
        InitPhysicsLayers();
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

    public void InitPhysicsLayers()
    {
        CollisionMask = new T().PhysicsLayer;
        CollisionLayer = 0;
    }
}