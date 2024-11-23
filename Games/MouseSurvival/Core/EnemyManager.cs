using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyManager : Node
{
	[Export] private int MaxEnemyCount = 1000;
	[Export] private float SpawnInterval = 1.0f;
	[Export] private float SpawnRadius = 2000.0f;
    [Export] private PackedScene Enemy;
    [Export] private int MovementGridCellSize = 8;

	private float TimeToNextSpawn;
	private List<Enemy> SpawnedEnemies = new List<Enemy>();
    private Dictionary<Vector2, bool> MovementGrid = new Dictionary<Vector2, bool>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        TimeToNextSpawn = SpawnInterval;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        SpawnEnemy();
        MoveEnemies();
    }

	private void SpawnEnemy()
	{
		if (SpawnedEnemies.Count < MaxEnemyCount)
		{
			TimeToNextSpawn -= (float)GetProcessDeltaTime();
            if (TimeToNextSpawn <= 0)
            {
				Enemy enemy = (Enemy)Enemy.Instantiate();
                AddChild(enemy);
                // Set the location of the enemy to a point along the edge of the spawn radius
                enemy.GlobalPosition = new Vector2(Mathf.Cos((float)GD.RandRange(0, Mathf.Pi * 2)) * SpawnRadius, Mathf.Sin((float)GD.RandRange(0, Mathf.Pi * 2)) * SpawnRadius);
                SpawnedEnemies.Add(enemy);
				enemy.OnEnemyKilled += OnEnemyKilled;
                TimeToNextSpawn = SpawnInterval;
            }
        }
    }

    private void MoveEnemies()
    {
        MovementGrid.Clear();

        foreach (Enemy enemy in SpawnedEnemies)
        {
            Vector2 direction = enemy.CalculateMovementTowardsMouse();
            Vector2 desiredPosition = enemy.GlobalPosition + direction;
            Vector2 gridPosition = new Vector2((int)(desiredPosition.X / MovementGridCellSize), (int)(desiredPosition.Y / MovementGridCellSize));
            if (!MovementGrid.ContainsKey(gridPosition))
            {
                MovementGrid.Add(gridPosition, true);
                enemy.GlobalPosition = desiredPosition;
            }
            //else
            //{
            //    direction /= 2;
            //    desiredPosition = enemy.GlobalPosition + direction;
            //    //enemy.GlobalPosition = desiredPosition;
            //    //gridPosition = new Vector2((int)(desiredPosition.X / MovementGridCellSize), (int)(desiredPosition.Y / MovementGridCellSize));

            //}
        }
    }

	private void OnEnemyKilled(Enemy enemy)
    {
        SpawnedEnemies.Remove(enemy);
    }
}
