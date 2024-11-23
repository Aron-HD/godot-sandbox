using Godot;
using System;

public partial class Boot : Node2D
{
    public BootState State { get; private set; } = BootState.SelectingScene;
    public delegate void StateChange(BootState state);
    public event StateChange OnStateChanged;

    private Node _instancedScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

    public void ChangeScene(PackedScene scene)
    {
        if (State == BootState.SelectingScene)
        {
           _instancedScene = scene.Instantiate();
            GetTree().Root.AddChild(_instancedScene);
            GetTree().Root.MoveChild(_instancedScene, 0);
            ChangeState(BootState.RunningScene);
        }
    }

    public void ChangeState(BootState state)
    {
        State = state;
        OnStateChanged.Invoke(State);

        switch (State)
        {
            case BootState.SelectingScene:
                if (_instancedScene != null)
                {
                    _instancedScene.QueueFree();
                }
                break;

            case BootState.RunningScene:
                break;
        }
    }

    // Make a boot state enum
    public enum BootState
    {
        SelectingScene,
        RunningScene
    }
}
