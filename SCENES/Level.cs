using Godot;
using System;

public class Level : Node2D
{
	CanvasLayer activePopup;
	Arrow arrow;
	GameUI sidebar;

	public bool ActiveMode {private set; get;} = false;
	private Entity activeEntity;
	private bool activeCoolingDown = false;


	public override void _Ready()
	{
		activePopup = GetNode<CanvasLayer>("ActivePopup");
		arrow = GetNode<Arrow>("Arrow");
		sidebar = GetNode<GameUI>("GameUI");

		activePopup.Hide();
	}

	public override void _Process(float delta)
	{
		if (ActiveMode && activeEntity != null)
		{
			Vector2 tilePosition = new Vector2((int)(GetGlobalMousePosition().x/16)*16, 
					(int)(GetGlobalMousePosition().y/16)*16);

			arrow.Visible = true;
			arrow.PointTo(GetTileManhattanDistance(tilePosition));
			arrow.Position = activeEntity.Position + new Vector2(6.5f, 16);
		}
		else
		{
			arrow.Visible = false;
		}
	}

	public void sig_ToActiveMode(Entity entity)
	{
		if (activeCoolingDown)
		{
			return;
		}
		// if (ActiveMode)
		// {
		// 	Deactivate();
		// }
		activePopup.Show();
		ActiveMode = true;
		activeEntity = entity;
		activeEntity.Active = true;
	}

	public void MoveEntityToTile(Vector2 pos)
	{
		activeEntity.WalkTo(new Vector2(pos.x + 2, pos.y - 10) - activeEntity.GlobalPosition);
		Deactivate();
		GetNode<Timer>("ActiveCooldown").Start();
		activeCoolingDown = true;
	}

	public void sig_ActiveCooldownFinished()
	{
		activeCoolingDown = false;
	}

	private void Deactivate()
	{
		ActiveMode = false;
		activeEntity.Active = false;
		activeEntity.Visible = true;
		activeEntity = null;
		activePopup.Hide();
	}

	public Vector2 GetTileManhattanDistance(Vector2 tilePos)
	{
		if (activeEntity == null)
		{
			return Vector2.Zero;
		}

		Vector2 tileDifference = new Vector2(tilePos.x + 2, tilePos.y - 10) - activeEntity.GlobalPosition;
		tileDifference /= 16;
		tileDifference = new Vector2(Mathf.Round(tileDifference.x), Mathf.Round(tileDifference.y));
		return tileDifference;
	}
}
