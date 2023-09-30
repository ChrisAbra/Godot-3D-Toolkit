namespace Godot3dToolkit;

public interface IInteractable<T> where T : Resource
{

    T duplicatedResource {get;}
    abstract void InteractionAccepted(IInteractor<T> interactor, Node interactingNode);
    abstract void InteractionRejected(IInteractor<T> interactor, Node interactingNode);
    void Handshake(IInteractor<T> interactor){
        if(!interactor.HandleHandshake(this)) {
            InteractionRejected(interactor, interactor.ReplyNode);
            return;
        }
        InteractionAccepted(interactor, interactor.ReplyNode);
    }

}