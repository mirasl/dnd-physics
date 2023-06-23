using Godot;
using System;

public class Arrow : Node2D
{
	Sprite x;
	Sprite y;
	Sprite arrowhead;


	public override void _Ready()
	{
		x = GetNode<Sprite>("X");
		y = GetNode<Sprite>("Y");
		arrowhead = GetNode<Sprite>("Arrowhead");

		PointTo(new Vector2(2, 3));
	}

    public void PointTo(Vector2 pos)
	{
		x.Scale = new Vector2(pos.x * 16 + 2 + 2*Mathf.Sign(pos.x), 4);
		y.Scale = new Vector2(4, Mathf.Abs(pos.y * 16 + 4) * Mathf.Sign(pos.y));
		y.Position = new Vector2(-2 + pos.x*16, -2);

		arrowhead.Visible = pos != Vector2.Zero;
		if (pos.y == 0)
		{
			arrowhead.FlipH = pos.x < 0;
			arrowhead.RotationDegrees = 0;
		}
		else
		{
			arrowhead.RotationDegrees = 90 * Mathf.Sign(pos.y);
			arrowhead.FlipH = false;
		}
		arrowhead.Position = pos * 16;
	}
}
