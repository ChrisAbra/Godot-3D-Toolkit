namespace Godot3dToolkit;

public interface IInteractable<T> where T : InteractableResource,  new()
{

    public T Resource {get;}
    public bool CloneResourceOnAccepted {get;init;}
    public T UniqueResource {get => (T)Resource?.Duplicate();}
    abstract void InteractionAccepted(IInteractor<T> interactor, Node interactingNode);
    abstract void InteractionRejected(IInteractor<T> interactor, Node interactingNode);
    void Handshake(IInteractor<T> interactor){
        if(!interactor.HandleHandshake(this)) {
            InteractionRejected(interactor, interactor.ReplyNode);
        }
        else {
            InteractionAccepted(interactor, interactor.ReplyNode);
        }
        return;
    }
    void InitPhysicsLayers();

}