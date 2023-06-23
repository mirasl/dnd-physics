using Godot;
using System;

public class Level : Node2D
{
	CanvasLayer activePopup;

	public bool ActiveMode = false;
	private Entity activeEntity;


	public override void _Ready()
	{
		activePopup = GetNode<CanvasLayer>("ActivePopup");

		activePopup.Hide();
	}

	public void sig_ToActiveMode(Entity entity)
	{
		activePopup.Show();
		ActiveMode = true;
		activeEntity = entity;
	}

	public void MoveEntityToTile(Vector2 pos)
	{
		activeEntity.Position = new Vector2(pos.x + 2, pos.y - 10);
	}
}
