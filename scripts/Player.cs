using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Player : CharacterBody2D
{
	[Export]
	private float Speed = 300.0f;
	[Export]
	private float JumpVelocity = -400.0f;
	[Export]
	private float GravityMultiplier = 2.0f;

	[Export]
	public PackedScene EquippedWeaponScene;

	private AnimatedSprite2D animatedSprite2D;
	private bool isSprinting = false;

    public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if(EquippedWeaponScene != null)
		{
			AddChild(EquippedWeaponScene.Instantiate());
		}

		
	}

    public override void _Process(double delta)
    {
		// Update the animation based on the player's movement
		if (Velocity.X != 0)
		{
			animatedSprite2D.Play("walk");
		}
		else
		{
			animatedSprite2D.Play("default");
		}

		// Flip the sprite based on the direction of movement
        if(Velocity.X > 0)
		{
			animatedSprite2D.FlipH = true;
		}
		else if(Velocity.X < 0)
		{
			animatedSprite2D.FlipH = false;
		}
    }



	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * GravityMultiplier * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		isSprinting = Input.IsActionPressed("sprint");
		Speed = isSprinting ? 450.0f : 300.0f; // Adjust speed based on sprinting state
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
