using Godot;
using System;

[GlobalClass]
public partial class Weapon : Node
{
	[Export]
	protected float Damage { set; get; }
	[Export]
	protected float UseTime { set; get; }
	[Export]
	protected int ClipSize { set; get; }

}
