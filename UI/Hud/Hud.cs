using Godot;
using System;

public partial class Hud : Control
{

    private Boot boot;

    private Control _sceneLoader;
    private Control _sceneRunner;
    private Button _backToMenuButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        // Check if the root node has a Boot node
        if (GetTree().Root.HasNode("Boot"))
        {
            // Get root node called Boot
            boot = GetTree().Root.GetNode<Boot>("Boot");
            boot.OnStateChanged += OnBootStateChanged;
        }

        _sceneLoader = GetNode<Control>("ScreenSizer/SceneLoader");
        _sceneRunner = GetNode<Control>("ScreenSizer/SceneRunner");

        _backToMenuButton = GetNode<Button>("ScreenSizer/SceneRunner/Btn_BackToMenu");
        _backToMenuButton.Pressed += () =>
        {
            boot.ChangeState(Boot.BootState.SelectingScene);
        };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnBootStateChanged(Boot.BootState state)
    {
        switch (state)
        {
            case Boot.BootState.SelectingScene:
                _sceneLoader.Show();
                _sceneRunner.Hide();
                break;

            case Boot.BootState.RunningScene:
                _sceneLoader.Hide();
                _sceneRunner.Show();
                break;
        }
    }
}
