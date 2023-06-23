using Godot;
using System;

public class Entity : Node2D
{
    [Signal] delegate void EntityDie();
	[Signal] delegate void ToActiveMode(Entity self);

    const float GRAVITY = 9.8f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int hp;
    public int currenthp;
    public int str;
    public int mass;
    public int stamina;
    public int damage;
    public int height;
    public Weapon weapon;


	public override void _PhysicsProcess(float delta)
	{
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
	}

    public enum Direction
    {
        Side,
        Top,
        Bottom,
        Stab
    }

    public float GetDamage(float length, float area, Direction choice, int distance)
    {
    	float damage = 0;
        if (choice == Direction.Side) 
		{
            damage = (weapon.Length * (Mathf.Pi / 2) * (str));
        } 
		else if (choice == Direction.Top) 
		{
            damage = (weapon.Length * (Mathf.Pi / 2) * (str) + (mass * GRAVITY * (height/2)));
        } 
		else if (choice == Direction.Bottom) 
		{
            damage = (weapon.Length * (Mathf.Pi / 2) * (str) - (mass * GRAVITY * (height/2)));
        } 
		else if (choice == Direction.Stab) 
		{
            damage = distance * str;
        }
        return damage;
    }

    public void ChangeHP()
    {
        currenthp -= damage;
        stamina -= damage;
        if (currenthp <= 0 || stamina <= 0) {
            EmitSignal("EntityDie");
        }
    }

	public bool MouseHovering()
	{
		Vector2 mousePos = GetLocalMousePosition();
		return mousePos.x < 14 && mousePos.x > 0 && mousePos.y > 0 && mousePos.y < 22;
	}
}
