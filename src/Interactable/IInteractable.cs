namespace Godot3dToolkit;

public interface IInteractable<T> where T : Resource
{

    T duplicatedResource {get;}
}