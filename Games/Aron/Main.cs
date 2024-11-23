using Godot;
using System;

namespace SurvivalSandbox.Games.Aron;


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

public partial class Main : Node2D
{
    private Button Quit;
    private Button Play;
    private Boot BootNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
         GD.Print("Main Node");
         BootNode = this.GetBootNode();
    }

    //// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnQuitGamePressed()
    {
        GD.Print("Quitting Game");
        var boot = GetTree().Root.GetNode<Boot>("Boot");
        boot?.ChangeState(BootState.SelectingScene);
    }

    public void OnPlayGamePressed()
    {
        GD.Print("Play Pressed");
        //GetTree().ChangeSceneToFile("res://Game.tscn");
    }
}