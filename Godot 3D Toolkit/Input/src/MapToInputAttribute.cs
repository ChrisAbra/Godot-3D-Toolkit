namespace Godot3dToolkit;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class MapToInputAttribute : Attribute
{
    public StringName ActionName;
    public bool Inverse;



    public MapToInputAttribute(string actionName, bool inverse = false)
    {
        ActionName = actionName;
        Inverse = inverse;
    }

    public StringName GetActionName(int playerIndex){
        return ActionName + "_" + playerIndex;
    }

}