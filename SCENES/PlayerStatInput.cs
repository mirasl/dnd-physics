using Godot;
using System;

public class PlayerStatInput : CanvasLayer
{

    public string weapon;

     [Signal] public delegate void sig_GetPlayerData();

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void sig_SaveButtonPressed() 
    {
        EmitSignal("sig_GetPlayerData");
    }

    // public void sig_OnWeaponButtonPressed()
    // {
    //     var weaponSelect = GD.Load<PackedScene>("res://SCENES/WeaponSelect.tscn");
    //     var WeaponSelect = weaponSelect.Instance<WeaponSelect>();
    //     AddChild(WeaponSelect);
    //     await ToSignal(WeaponSelect, "sig_WeaponChosen");
    //     WeaponSelect.Connect("sig_");
    // }

    public void deleteScene()
    {
        QueueFree();
    }
}
