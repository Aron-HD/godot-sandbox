using Godot;
using System;

public partial class Boot : Node2D
{
    // Reference to scene
    private PackedScene _scene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        // Load a scene called chungus from res/scenes/Chungus.tscn
        _scene = GD.Load<PackedScene>("res://scenes/Chungus.tscn");
        // Open the scene
        Node sceneInstance = _scene.Instantiate();
        AddChild(sceneInstance);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
