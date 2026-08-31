using Godot;
using System;

public partial class BasicGun : Node2D
{
	[Export]
	private PackedScene bulletScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mousePos = GetGlobalMousePosition();
		LookAt(mousePos);

		if(Input.IsActionJustPressed("Shoot"))
		{
			GD.Print("Shoot pressed");
			Vector2 bulletSpawnPos = Position;
			float bulletSpawnDir = Rotation;
			Node2D bulletNode = bulletScene.Instantiate<Node2D>();
			bulletNode.Position = bulletSpawnPos;
			bulletNode.Rotation = bulletSpawnDir;
			GetParent().AddChild(bulletNode);
		}
	}
}
