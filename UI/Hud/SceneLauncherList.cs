using Godot;
using System.Collections.Generic;
using System.IO;

namespace SurvivalSandbox.UI.Hud;

[Tool]
public partial class SceneLauncherList : VBoxContainer
{
    // List of strings
    [Export] public string[] ScenePaths;

    private readonly List<PackedScene> scenes = new();
    private Boot bootNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var rootWindow = GetTree().Root;

        foreach (string scenePath in ScenePaths)
        {
            scenes.Add(GD.Load<PackedScene>(scenePath));
        }

        if (rootWindow.HasNode("Boot"))
        {
            bootNode = rootWindow.GetNode<Boot>("Boot");
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
            Button button = new()
            {
                Text = Path.GetFileNameWithoutExtension(scene.ResourcePath)
            };

            button.Pressed += () => ChangeScene(scene);

            AddChild(button);
        }
    }

    private void ChangeScene(PackedScene scene)
    {
        bootNode?.ChangeScene(scene);
    }
}
