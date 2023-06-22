using Godot;
using System;

public class Camera : Camera2D
{
	const float SPEED = 2;

	public override void _PhysicsProcess(float delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (Input.IsActionPressed("up"))
		{
			velocity.y -= SPEED;
		}
		else if (Input.IsActionPressed("down"))
		{
			velocity.y += SPEED;
		}

		if (Input.IsActionPressed("right"))
		{
			velocity.x += SPEED;
		}
		else if (Input.IsActionPressed("left"))
		{
			velocity.x -= SPEED;
		}

		if (Input.IsActionPressed("slow"))
		{
			velocity *= 2;
		}

		if (Input.IsActionJustPressed("zoom_out"))
		{
			Zoom += Vector2.One*0.2f;
		}
		else if (Input.IsActionJustPressed("zoom_in"))
		{
			Zoom -= Vector2.One*0.2f;
		}
		Position += velocity;
	}
}
