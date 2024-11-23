using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export] public float MoveSpeed = 1.0f;
    [Export] public float CollisionRadius = 1.0f;

    // EnemyKilledDelegate
    public delegate void EnemyKilled(Enemy enemy);
    public event EnemyKilled OnEnemyKilled;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //MoveTowardsMouse();
        CheckCollision();
    }

	public Vector2 CalculateMovementTowardsMouse()
	{
        // Get mouse position
        Vector2 mousePosition = GetGlobalMousePosition();
        // Get the direction to the mouse
        Vector2 direction = (mousePosition - GlobalPosition).Normalized();
        // Move towards the mouse adjusted by delta time
        // GlobalPosition += direction * MoveSpeed * (float)GetProcessDeltaTime();
        Vector2 movement = direction * MoveSpeed * (float)GetProcessDeltaTime();
        return movement;
    }

    private void CheckCollision()
    {
        // Get mouse position
        Vector2 mousePosition = GetGlobalMousePosition();
        // Get the distance to the mouse
        float distance = GlobalPosition.DistanceTo(mousePosition);
        // Check if the distance is less than the collision radius
        if (distance < CollisionRadius)
        {
            // destroy self
            QueueFree();
            // Call the OnEnemyKilled event
            OnEnemyKilled.Invoke(this);
        }
    }
}
