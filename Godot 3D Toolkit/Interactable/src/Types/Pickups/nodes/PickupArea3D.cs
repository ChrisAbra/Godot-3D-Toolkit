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
    public StringName AnimationOnAccepted {get;set;}
    [Export]
    public StringName AnimationOnRejected {get;set;}

    bool hasAcceptedAnimation {
        get => Animator?.HasAnimation(AnimationOnAccepted) == true;
    }
    bool hasRejectedAnimation {
        get => Animator?.HasAnimation(AnimationOnAccepted) == true;
    }


    [Export]
    public Node QueueFreeOnCollection {get;set;}

    public override Pickup Resource => (Pickup)Pickup.Duplicate(true);

    public override void InteractionAccepted(IInteractor<Pickup> interactor, Node node)
    {
        EmitSignal(SignalName.Collected, Pickup, node);

        QueueFreeOnCollection?.QueueFree(); 

        if(hasAcceptedAnimation) Animator.Play(AnimationOnAccepted);
        
    }

    public override void InteractionRejected(IInteractor<Pickup> interactor, Node node){

        if(hasRejectedAnimation) Animator.Play(AnimationOnRejected);

    }

}