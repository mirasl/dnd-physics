using Godot;
using System;

public class Weapon : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int mass {get;set;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public int getLength(int l) {
        return l;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
