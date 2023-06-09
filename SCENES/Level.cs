using Godot;
using System;

public class Level : Node2D
{
	CanvasLayer activePopup;
	Arrow arrow;
	GameUI sidebar;
	AttackScreen attackScreen;
	CanvasLayer mainMenu;
	Node2D entities;

	public bool ActiveMode {private set; get;} = false;
	private Entity activeEntity;
	private Entity activeOpponent;
	private bool activeCoolingDown = false;
	private bool threatening = false;

    public enum Direction
    {
        Side,
        Top,
        Bottom,
        Stab
    }


	public override void _Ready()
	{
		activePopup = GetNode<CanvasLayer>("ActivePopup");
		arrow = GetNode<Arrow>("Arrow");
		sidebar = GetNode<GameUI>("GameUI");
		attackScreen = GetNode<AttackScreen>("AttackScreen");
		mainMenu = GetNode<CanvasLayer>("MainMenu");
		entities = GetNode<Node2D>("Entities");

		mainMenu.Connect("sig_GetPlayerStats", this, "GetPlayerData");

		activePopup.Hide();
		attackScreen.Hide();
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

		if (threatening)
		{
			arrow.Modulate = Colors.DarkRed;
		}
		else
		{
			arrow.Modulate = Colors.LightBlue;
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
		sidebar.SetValues(activeEntity);
	}

	public void MoveEntityToTile(Vector2 pos)
	{
		if (threatening)
		{
			InitiateBattle();
		}
		else
		{
			activeEntity.WalkTo(new Vector2(pos.x + 2, pos.y - 10) - activeEntity.GlobalPosition);
		}
		Deactivate();
		threatening = false;
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
		if (activeEntity is null)
		{
			return Vector2.Zero;
		}

		Vector2 tileDifference = new Vector2(tilePos.x + 2, tilePos.y - 10) - activeEntity.GlobalPosition;
		tileDifference /= 16;
		tileDifference = new Vector2(Mathf.Round(tileDifference.x), Mathf.Round(tileDifference.y));
		return tileDifference;
	}

	public void sig_Threatened(bool state, Entity opponent)
	{
		threatening = state;
		if (threatening)
		{
			activeOpponent = opponent;
		}
		else
		{
			activeOpponent = null;
		}
	}


	public void InitiateBattle()
	{

		attackScreen.Show();
		Vector2 distance = new Vector2((int)(GetGlobalMousePosition().x/16)*16, (int)(GetGlobalMousePosition().y/16)*16);
		distance = GetTileManhattanDistance(distance);
		int totalDistance = Mathf.Abs((int)distance.x) + Mathf.Abs((int)distance.y);
		attackScreen.PrepBattle(activeEntity, activeOpponent, totalDistance);
	}

	public void GetPlayerData()
	{
		PlayerStatInput playerStat = GetNode<PlayerStatInput>("PlayerStatInput");
		CreateEntity(playerStat);
		playerStat.deleteScene();
		GD.Print("Data Got!");
	}

	private void CreateEntity(PlayerStatInput playerStat)
	{
		PackedScene entityScene = GD.Load<PackedScene>("res://SCENES/Entity.tscn");
		Entity entity = entityScene.Instance<Entity>();

		entity.name = playerStat.GetNode<LineEdit>("GridContainer/NameInput").Text;
		entity.hp = float.Parse(playerStat.GetNode<LineEdit>("GridContainer/HealthInput").Text);
		entity.stamina = float.Parse(playerStat.GetNode<LineEdit>("GridContainer/StaminaInput").Text);
		entity.str = (int)float.Parse(playerStat.GetNode<LineEdit>("GridContainer/StrengthInput").Text);
		entity.mass = float.Parse(playerStat.GetNode<LineEdit>("GridContainer/MassInput").Text);
		entity.height = float.Parse(playerStat.GetNode<LineEdit>("GridContainer/HeightInput").Text);

		entities.AddChild(entity);
		entity.Connect("Threatened", this, "sig_Threatened");
		entity.Connect("ToActiveMode", this, "sig_ToActiveMode");
		GD.Randomize();
		entity.Position = new Vector2(GD.Randi()%68 * 16 + 2, GD.Randi()%41 * 16 - 10);
	}
}
