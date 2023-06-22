using Godot;
using System;

public class GridCell : AnimatedSprite
{
	public override void _PhysicsProcess(float delta)
	{
		if (MouseHovering())
		{
			Animation = "hover";
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
