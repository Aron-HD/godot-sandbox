using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public partial class FireTrail : Line2D
{
	[Export] private int MaxLength = 10;
	[Export] private bool FollowMouse = true;
	[Export] float MinimumDistance = 10f;

    private List<Vector2> _points = new List<Vector2>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 targetPosition;

		if (FollowMouse)
		{
            targetPosition = GetGlobalMousePosition();
        }
		else
		{
            // Get parent node
            Node2D parent = GetParent<Node2D>();
            targetPosition = parent.GlobalPosition;
        }

        if (_points.Count == 0 || targetPosition.DistanceTo(_points[0]) > MinimumDistance)
        {
            _points.Insert(0, targetPosition);

            if (_points.Count > MaxLength)
            {
                _points.RemoveAt(_points.Count - 1);
            }
        }
        else
        {
            _points.RemoveAt(_points.Count - 1);
        }

		ClearPoints();
		for (int i = 0; i < _points.Count; i++)
        {
            Vector2 point = _points[i];
            AddPoint(point);
        }
    }
}
