using System;

namespace Godot3dToolkit;

[GlobalClass]
[Icon("uid://dqcqowp6gtfvh")]
public partial class ResourceState : Node
{
    [Export]
    public Resource Resource;

    [Export]
    public ResourceState DefaultSubstate;
    public ResourceState ActiveSubstate;

    public Array<ResourceStateModifer> Modifiers;

}
