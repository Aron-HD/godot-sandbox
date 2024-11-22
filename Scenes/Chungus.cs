using Godot;
using System;

[Tool]
public partial class Chungus : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Set the size of this to the size of the window
        this.CustomMinimumSize = GetViewportRect().Size;

        TextureRect textureRect = GetNode<TextureRect>("Panel/TextureRect");
        // set the custom minimum size to the current size
        textureRect.CustomMinimumSize = textureRect.Size;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        // Get grandchild texturerect
        TextureRect textureRect = GetNode<TextureRect>("Panel/TextureRect");

        // if user presses space
        if (Input.IsKeyPressed(Key.Space))
        {
            // Scale the texture rect by 1.1
            textureRect.CustomMinimumSize *= 1.1f;
        }

    }
}
