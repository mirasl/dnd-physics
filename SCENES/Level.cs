using Godot;
using System;

public class Level : Node2D
{
	CanvasLayer activePopup;

	public bool ActiveMode {private set; get;} = false;
	private Entity activeEntity;
	private bool activeCoolingDown = false;


	public override void _Ready()
	{
		activePopup = GetNode<CanvasLayer>("ActivePopup");

		activePopup.Hide();
	}

	public void sig_ToActiveMode(Entity entity)
	{
		if (activeCoolingDown)
		{
			return;
		}
		activePopup.Show();
		ActiveMode = true;
		activeEntity = entity;
	}

	public void MoveEntityToTile(Vector2 pos)
	{
		activeEntity.GlobalPosition = new Vector2(pos.x + 2, pos.y - 10);
		ActiveMode = false;
		activeEntity = null;
		activePopup.Hide();
		GetNode<Timer>("ActiveCooldown").Start();
		activeCoolingDown = true;
	}

	public void sig_ActiveCooldownFinished()
	{
		activeCoolingDown = false;
	}
}
