using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SurvivalSandbox.Games.Aron;

public enum MovementState
{
    Idle,
    //Walking,
    Running,
    Sprinting,
    Jumping,
    Falling
}

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;
    public const int MaxJumps = 2;

    private AnimationPlayer _anim;
    private AnimatedSprite2D _sprite;

    private int _currentJumps = 0;
    private List<MovementState> _currentMovementStates = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _currentMovementStates.Add(MovementState.Idle);
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        _anim.SpeedScale = 1;
        Vector2 velocity = Velocity;

        float thisMoveSpeed = Input.IsActionPressed("sprint") ? Speed * 2 : Speed;

        bool onFloor = IsOnFloor();


        // Add the gravity.
        if (!onFloor)
        {
            velocity += GetGravity() * (float)delta;
        }


        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        if (direction.X < 0)
            _sprite.SetFlipH(true);
        else if (direction.X > 0)
            _sprite.SetFlipH(false);



        if (velocity.Y == 0)
        {
            HandleLanding();
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump"))
        {
            bool success = SetJumping();
            if (success) velocity.Y = JumpVelocity;
        }
        else if (velocity.Y > 0)
        {
            SetFalling();
        }

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * thisMoveSpeed;
            if (onFloor) SetRunning();
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, thisMoveSpeed);
            if (onFloor) SetIdle();
        }

        HandleMovementState();

        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleLanding()
    {
        _currentJumps = 0;
        RemoveMovementStates(MovementState.Falling);
    }

    private void SetIdle()
    {
        if (_currentMovementStates.Contains(MovementState.Jumping) || _currentMovementStates.Contains(MovementState.Falling))
            return;

        AddMovementState(MovementState.Idle);

        RemoveMovementStates(new[] {
            MovementState.Running,
            MovementState.Sprinting,
            MovementState.Jumping,
            MovementState.Falling
        });

        GD.Print(States);
        _anim.Play("Idle");
    }

    private bool SetJumping()
    {
        if (!IsOnFloor() && _currentJumps >= MaxJumps)
        {
            return false;
        }

        if (_currentJumps < MaxJumps)
        {
            AddMovementState(MovementState.Jumping);

            RemoveMovementStates(new[] {
                MovementState.Idle,
                MovementState.Running,
                MovementState.Sprinting,
                MovementState.Falling
            });

            _anim.Play("Jump");

            _currentJumps++;
            GD.Print(States);
            return true;
        }
        return false;
    }

    string States => string.Join(", ", _currentMovementStates.Select(m => m.ToString()));

    private void SetFalling()
    {
        if (AddMovementState(MovementState.Falling)) return;

        RemoveMovementStates(new[] {
            MovementState.Idle,
            MovementState.Running,
            MovementState.Sprinting,
            MovementState.Jumping
        });

        GD.Print(States);

        _anim.Play("Fall");
    }

    private void SetRunning()
    {
        if (_currentMovementStates.Contains(MovementState.Jumping) || _currentMovementStates.Contains(MovementState.Falling))
            return;

        RemoveMovementStates(MovementState.Idle);
        if (Input.IsActionPressed("sprint"))
        {
            RemoveMovementStates(MovementState.Running);
            AddMovementState(MovementState.Sprinting);
            _anim.SpeedScale = 2;
        }
        else
        {
            AddMovementState(MovementState.Running);
        }

        GD.Print(States);
        _anim.Play("Run");
    }

    private bool AddMovementState(MovementState state)
    {
        bool isExist = _currentMovementStates.Contains(state);
        if (!isExist) _currentMovementStates.Add(state);
        return isExist;
    }

    private void RemoveMovementStates(MovementState state) => _currentMovementStates.Remove(state);
    private void RemoveMovementStates(MovementState[] states)
    {
        foreach (var item in states)
        {
            _currentMovementStates.Remove(item);
        }
    }

    private void HandleMovementState()
    {
        if (IsOnFloor())
        {
            if (_currentMovementStates.Contains(MovementState.Jumping) || _currentMovementStates.Contains(MovementState.Falling))
            {
                _currentMovementStates.Remove(MovementState.Jumping);
                _currentMovementStates.Remove(MovementState.Falling);
            }
        }
    }

}
