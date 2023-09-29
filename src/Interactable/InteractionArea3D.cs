﻿namespace Godot3dToolkit;
public abstract partial class InteractionArea3D<T> : Area3D, IInteractable<T> where T : Resource
{

    public abstract T duplicatedResource {get;}

    public InteractionArea3D(){
        AreaEntered += OnAreaEntered;
        BodyEntered += OnBodyEntered;
    }

    public virtual void OnAreaEntered(Area3D enteringArea){

        if(enteringArea is not IInteractor<T> interactor) return;

        interactor.Interact(duplicatedResource, this);

    }

    public virtual void OnBodyEntered(Node enteringBody){
        if(enteringBody is not IInteractor<T> interactor) return;
        interactor.Interact(duplicatedResource, this);
    }



}