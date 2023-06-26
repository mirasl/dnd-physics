using Godot;
using System;

public class Camera : Camera2D
{
	const float SPEED = 2;

	public override void _PhysicsProcess(float delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (Input.IsActionPressed("up") && Position.y > 100)
		{
			velocity.y -= SPEED;
		}
		else if (Input.IsActionPressed("down") && Position.y < 500)
		{
			velocity.y += SPEED;
		}

		if (Input.IsActionPressed("right") && Position.x < 900)
		{
			velocity.x += SPEED;
		}
		else if (Input.IsActionPressed("left") && Position.x > 100)
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
		Position += velocity * Zoom.x;
	}
}
