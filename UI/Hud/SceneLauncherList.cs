using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.IO;

[Tool]
public partial class SceneLauncherList : VBoxContainer
{
    // List of strings
    [Export] public string[] ScenePaths;

    private List<PackedScene> scenes = new List<PackedScene>();
    private Boot boot;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        foreach (string scenePath in ScenePaths)
        {
            scenes.Add(GD.Load<PackedScene>(scenePath));
        }

        // Check if the root node has a Boot node
        if (GetTree().Root.HasNode("Boot"))
        {
            // Get root node called Boot
            boot = GetTree().Root.GetNode<Boot>("Boot");
        }

        PopulateButtonsFromScenes();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
    }

	private void PopulateButtonsFromScenes()
    {
        // Clear all children
        foreach (Node child in GetChildren())
        {
            child.QueueFree();
        }

        // Add a button for each scene
        foreach (PackedScene scene in scenes)
        {
            Button button = new Button();
            // Strip the path and extension from the scene path
            button.Text = Path.GetFileNameWithoutExtension(scene.ResourcePath);
            button.Pressed += () =>
            {
                ChangeScene(scene);
            };
            AddChild(button);
        }
    }

    private void ChangeScene(PackedScene scene)
    {
        if (boot != null)
        {
            boot.ChangeScene(scene);
        }
    }
}
