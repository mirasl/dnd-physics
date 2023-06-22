using Godot;
using System;

public class Entity : Spatial
{
    [Signal] delegate void EntityDie();
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
    


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
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
        if (choice == Direction.Side) {
            damage = (weapon.Length * (Mathf.Pi / 2) * (str));
        } else if (choice == Direction.Top) {
            damage = (weapon.Length * (Mathf.Pi / 2) * (str) + (mass * GRAVITY * (height/2)));
        } else if (choice == Direction.Bottom) {
            damage = (weapon.Length * (Mathf.Pi / 2) * (str) - (mass * GRAVITY * (height/2)));
        } else if (choice == Direction.Stab) {
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

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
