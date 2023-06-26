using Godot;
using System;

public class Entity : Node2D
{
    [Signal] delegate void EntityDie();
	[Signal] delegate void ToActiveMode(Entity self);
	[Signal] delegate void Threatened(bool state, Entity self);

	[Export] string SpriteName = "Player";

    const float GRAVITY = 9.8f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	public string name = "Jeff";
    public float hp = 10;
    public float currenthp = 10;
    public int str = 50;
    public float mass = 50;
    public float stamina = 100;
    public float height = 1.6f;
	public float weaponLength = 1;
	public float weaponMass = 10;
	public string weaponName = "Sword";

	Tween tween;
	AnimatedSprite currentAnimatedSprite;
	Node2D cell;
	Level level;

	public bool Active = false;
	float time = 0;
	private bool threatened = false;


	public override void _Ready()
	{
		tween = GetNode<Tween>("Tween");
		currentAnimatedSprite = GetNode<AnimatedSprite>(SpriteName);
		cell = GetNode<Node2D>("Cell");
		level = GetNode<Level>("/root/Level"); // !!! BAD TOO BAD LOL

		cell.Hide();
		currentAnimatedSprite.Show();
	}

	public override void _PhysicsProcess(float delta)
	{
		time += delta;

		cell.GlobalPosition = new Vector2((int)(cell.GlobalPosition.x/16)*16, (int)(cell.GlobalPosition.x/16)*16);

		if (MouseHovering())
		{
			Modulate = new Color(0.5f,0.5f,0.5f);
			if (Input.IsActionJustPressed("leftclick"))
			{
				EmitSignal("ToActiveMode", this);
			}
		}
		else
		{
			Modulate = Colors.White;
		}

		if (Active)
		{
			Visible = (int)(time*4)%2 == 0;
		}
		
		if (!Active && level.ActiveMode && MouseHoveringOverTile() && !threatened)
		{
			threatened = true;
			EmitSignal("Threatened", true, this);
			//cell.Show();
		}
		else if (threatened && !MouseHoveringOverTile() && !Active && level.ActiveMode)
		{
			threatened = false;
			cell.Hide();
			EmitSignal("Threatened", false, this);
		}
	}

	public bool MouseHovering()
	{
		Vector2 mousePos = GetLocalMousePosition();
		return mousePos.x < 14 && mousePos.x > 0 && mousePos.y > 0 && mousePos.y < 22;
	}

	private bool MouseHoveringOverTile()
	{
		Vector2 mousePos = GetLocalMousePosition();
		return mousePos.x < 16 && mousePos.x > -1 && mousePos.y > 9 && mousePos.y < 24;
	}

	public async void WalkTo(Vector2 pos)
	{
		float xDestination = Position.x + pos.x;
		if (pos.x < 0)
		{
			xDestination += 1;
		}
		tween.InterpolateProperty(this, "position", Position, new Vector2(xDestination, 
				Position.y), Mathf.Abs(pos.x) / 96);
		tween.Start();

		currentAnimatedSprite.Play("walkright");
		currentAnimatedSprite.FlipH = pos.x < 0;

		await ToSignal(tween, "tween_completed");

		tween.InterpolateProperty(this, "position", Position, new Vector2(Position.x, Position.y + 
				pos.y), Mathf.Abs(pos.y) / 96);
		tween.Start();

		if (pos.y > 0)
		{
			currentAnimatedSprite.Play("walkdown");
		}
		else
		{
			currentAnimatedSprite.Play("walkup");
		}

		await ToSignal(tween, "tween_completed");

		currentAnimatedSprite.Play("idle");
	}
}
