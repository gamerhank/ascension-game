using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Player : CharacterBody2D
{
	[Export]
	private float walkSpeed = 300.0f;
	[Export]
	private float sprintSpeed = 450.0f;
	[Export]
	private float JumpVelocity = -400.0f;
	[Export]
	private float GravityMultiplier = 2.0f;
	[Export]
	private Vector2 weaponToHandOffset = new(0, 0);
	[Export]
	public PackedScene EquippedWeaponScene;

	private Node2D flipPivot;
	private AnimatedSprite2D animatedSprite2D;
	private bool isSprinting = false;

    public override void _Ready()
	{
		// Get child nodes
		flipPivot = GetNode<Node2D>("FlipPivot");
		animatedSprite2D = flipPivot.GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Instantiate player weapon
		if(EquippedWeaponScene != null)
		{
			Node2D EquippedWeapon = EquippedWeaponScene.Instantiate<Node2D>();
			EquippedWeapon.Position = weaponToHandOffset; // Set offset to align with player sprite
			flipPivot.AddChild(EquippedWeapon);
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

		// Flip the player based on the direction of movement
		if (Velocity != Vector2.Zero)
		{
			flipPivot.Scale = flipPivot.Scale with { X = Velocity.X > 0 ? -1 : 1 };	
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
		var Speed = isSprinting ? sprintSpeed : walkSpeed; // Adjust speed based on sprinting state
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
