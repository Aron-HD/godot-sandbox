using Godot;
using System;

[Tool]
public partial class ScreenSizeControl : Container
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateSize();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateSize();
    }

	private void UpdateSize()
    {
        // If in editor, update the size to the project setting's height and width
        if (Engine.IsEditorHint())
        {
            Vector2 size = new Vector2((int)ProjectSettings.GetSetting("display/window/size/viewport_width"), (int)ProjectSettings.GetSetting("display/window/size/viewport_height"));
            this.CustomMinimumSize = size;
        }
        else
        {
            // Set the size of this to the size of the window
            this.CustomMinimumSize = GetViewportRect().Size;
        }
    }
}
