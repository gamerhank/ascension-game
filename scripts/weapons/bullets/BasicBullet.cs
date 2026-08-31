using Godot;
using System;

public partial class BasicBullet : Node2D
{
	[Export]
	public float BulletSpeedMultiplier = 10.0f;

	private VisibleOnScreenNotifier2D notifier;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		notifier = GetNode<VisibleOnScreenNotifier2D>("Visible");
		// if(!notifier.IsOnScreen())
		// {
		// 	GD.Print("Removed immediately");
		// 	OnScreenExited();
		// }
		notifier.ScreenExited += OnScreenExited;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Transform.X * 10.0f;
	}

	private void OnScreenExited()
	{
		GD.Print("Bullet node removed");
		// QueueFree();
	}
}
