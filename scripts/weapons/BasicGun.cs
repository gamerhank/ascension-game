using Godot;
using System;
using System.Security.Cryptography;

public partial class BasicGun : Node2D
{
	[Export]
	private PackedScene bulletScene;

	private Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mousePos = GetGlobalMousePosition();
		LookAt(mousePos);

		if(Input.IsActionJustPressed("Shoot"))
		{
			spawnBullet();
		}

		// GD.Print(Transform.X.X);
		Scale = Scale with { Y = Transform.X.X < 0.0f ? -1 : 1 };
	}

	private void spawnBullet()
	{
		// GD.Print("Shoot pressed, rotation=", Rotation);
		Vector2 bulletSpawnPos = GlobalPosition;
		float bulletSpawnDir = GlobalRotation;
		Node2D bulletNode = bulletScene.Instantiate<Node2D>();
		// bulletNode.Position = bulletSpawnPos;
		// GD.Print("bullet rotation=", bulletNode.Rotation);
		GetTree().CurrentScene.AddChild(bulletNode);

		bulletNode.GlobalPosition = bulletSpawnPos;
		bulletNode.Rotation = bulletSpawnDir;
	}
}
