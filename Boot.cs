using Godot;

namespace SurvivalSandbox;

public enum BootState
{
    SelectingScene,
    RunningScene
}

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
        var rootWindow = GetTree().Root;
        if (State == BootState.SelectingScene)
        {
            _instancedScene = scene.Instantiate();
            rootWindow.AddChild(_instancedScene);
            rootWindow.MoveChild(_instancedScene, 0);
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
                _instancedScene?.QueueFree();
                break;
            case BootState.RunningScene:
                break;
        }
    }
}
