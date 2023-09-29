namespace Godot3dToolkit;
[GlobalClass]
public partial class PickupArea3D : InteractionArea3D<Pickup>
{

    [Signal]
    public delegate void CollectedEventHandler(Pickup pickup, Node collector);

    [Export]
    public Pickup Pickup {get;set;}

    [Export]
    public AnimationPlayer Animator {get;set;}
    [Export]
    public StringName AnimationOnCollection {get;set;} = "";
    [Export]
    public Node QueueFreeOnCollection {get;set;}

    public override Pickup duplicatedResource => (Pickup)Pickup.Duplicate(true);

    public override void OnInteraction(IInteractor<Pickup> interactor, Node node){

        base.OnInteraction(interactor,node);

        EmitSignal(SignalName.Collected, Pickup, node);

        if(QueueFreeOnCollection is not null) QueueFreeOnCollection.QueueFree(); 

        if(Animator is null) return;
        if(AnimationOnCollection is null) return;
        Animator.Play(AnimationOnCollection);
        
    }

}