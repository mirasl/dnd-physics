using Godot;
using System;

public class MainMenu : CanvasLayer
{

[Signal] public delegate void sig_GetPlayerStats();

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public CanvasLayer StatInput;

    public override void _Ready()
    {
        
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void sig_AddPlayerPressed() 
    {
        var PlayerStat = GD.Load<PackedScene>("res://SCENES/PlayerStatInput.tscn");
        var playerStat = PlayerStat.Instance<PlayerStatInput>();
        GetParent().AddChild(playerStat);
        StatInput = playerStat;
        StatInput.Show();
        StatInput.Connect("sig_GetPlayerData", this, "GetPlayerData");
        

    }

    public void GetPlayerData()
    {
        EmitSignal("sig_GetPlayerStats");
        //StatInput.Disconnect("sig_GetPlayerStats", this, "sig_GetPlayerStats");
    }

    public void sig_OnDoneButtonPressed()
    {
        QueueFree();
    }
}
