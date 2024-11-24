using Godot;
using System;

namespace SurvivalSandbox.Common;

internal class Helpers
{
}

public static class NodeExtensions
{
    public static Boot GetBootNode(this Node node)
    {
        var x = node.GetTree().Root;
        if (x.HasNode("Boot"))
        {
            return x.GetNode<Boot>("Boot");
        }
        else throw new NullReferenceException("Boot node not found");
    }
}

