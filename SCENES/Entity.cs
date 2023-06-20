using Godot;
using System;

public class Entity : Spatial
{
    const float GRAVITY = 9.8f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int hp;
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

    public int GetDamage(float length, float area, int choice, int distance)
    {
        int damage;
        if (choice == Direction.Side) {
            damage = (weapon.getLength(length) * (Mathf.Pi / 2) * (str)) / area;
        } else if (choice == Direction.Top) {
            damage = (weapon.getLength(length) * (Mathf.Pi / 2) * (str) + (mass * GRAVITY * (height/2))) / area;
        } else if (choice == Direction.Bottom) {
            damage = (weapon.getLength(length) * (Mathf.Pi / 2) * (str) - (mass * GRAVITY * (height/2))) / area;
        } else if (choice == Direction.Stab) {
            damage = distance * str;
        }
        return damage;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
