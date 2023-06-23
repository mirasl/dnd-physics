using Godot;
using System;

public class GridCell : AnimatedSprite
{
	Level level;


	public override void _Ready()
	{
		level = GetNode<Level>("/root/Level"); // !!! BAD CODE ALERT, TOO BAD LMAO
	}

	public override void _PhysicsProcess(float delta)
	{
		if (MouseHovering() && level.ActiveMode)
		{
			Animation = "hover";
			if (Input.IsActionJustPressed("leftclick"))
			{
				level.MoveEntityToTile(GlobalPosition);
			}
		}
		else
		{
			Animation = "normal";
		}
	}

	public bool MouseHovering()
	{
		Vector2 mousePos = GetLocalMousePosition();
		return mousePos.x < 16 && mousePos.x > 0 && mousePos.y > 0 && mousePos.y < 16;
	}
}
