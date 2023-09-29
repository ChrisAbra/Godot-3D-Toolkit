namespace Godot3dToolkit;
public abstract partial class InteractionArea3D<T> : Area3D, IInteractable<T> where T : Resource
{

    public abstract T duplicatedResource {get;}

    public InteractionArea3D(){
        Monitorable = false;
        AreaEntered += (area) => {
            if(area is not IInteractor<T> interactor) return;
            OnInteraction(interactor, area);
        };
        BodyEntered += (node) => {
            if(node is not IInteractor<T> interactor) return;
            OnInteraction(interactor, node);
        };
    }

    public virtual void OnInteraction(IInteractor<T> interactor, Node node){
        interactor.Interact(duplicatedResource, this);
    }

}