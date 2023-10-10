using System.Linq;
using System.Reflection;

namespace Godot3dToolkit.InputManager;

struct Action
{
    public string MemberName;
    public MapToInputAttribute Attribute;
    public Type MemberType;
    public PropertyInfo PropInfo;
    public FieldInfo FieldInfo;

}
public abstract partial class PlayerController : Node
{
    public abstract int PlayerIndex {get;set;}

    List<Action> _buttons = new();
    List<Action> _triggers = new();
    List<Action> _joysticks = new();

    public override void _Ready()
    {
        var _mappedMemebers = GetType().GetMembers()
            .Where(member => member.IsDefined(typeof(MapToInputAttribute), false));

        var validActions = InputMap.GetActions();

        foreach (var member in _mappedMemebers)
        {
            var attribute = member.GetCustomAttribute<MapToInputAttribute>();
            if(!validActions.Contains(attribute.GetActionName(PlayerIndex))) continue;

            Type type;
            Action action = new()
            {
                MemberName = member.Name,
                Attribute = attribute
            };

            if (member is FieldInfo fieldInfo)
            {
                type = fieldInfo.FieldType;
                action = action with { FieldInfo = fieldInfo };
            }
            else if (member is PropertyInfo propInfo)
            {
                type = propInfo.PropertyType;
                action = action with { PropInfo = propInfo };
            }
            else continue;

            action = action with { MemberType = type };




            if (type == typeof(bool)) _buttons.Add(action);
            else if (type == typeof(Vector2)) _joysticks.Add(action);
            else if (type == typeof(float)) _triggers.Add(action);


        }


    }

    public override void _UnhandledInput(InputEvent @event)
    {
        foreach (var button in _buttons)
        {
            if (@event.IsActionPressed(button.Attribute.GetActionName(PlayerIndex)))
            {
                button.FieldInfo?.SetValue(this, true);
                button.PropInfo?.SetValue(this, true);
            }
            else if (@event.IsActionReleased(button.Attribute.GetActionName(PlayerIndex)))
            {
                button.FieldInfo?.SetValue(this, false);
                button.PropInfo?.SetValue(this, false);
            }
        }
    }

}