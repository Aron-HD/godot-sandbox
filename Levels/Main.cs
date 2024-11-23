using Godot;

namespace SurvivalSandbox.Levels;

public partial class Main : Node2D
{
    Button Quit;
    Button Play;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
         GD.Print("Main Node");
         //Quit = GetNode<Button>("Quit");
         //GD.Print(Quit.ToString());
        //Quit.Pressed += () => OnQuitPressed();

        //Play = GetNode<Button>("Play");
        //Play.Pressed += () => OnPlayPressed();
    }

    //// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnQuitGamePressed()
    {
        GD.Print("Quitting Game");
    }

    public void OnPlayGamePressed()
    {
        GD.Print("Play Pressed");
        //GetTree().ChangeSceneToFile("res://Game.tscn");
    }
}
