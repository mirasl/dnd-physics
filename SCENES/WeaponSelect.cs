using Godot;
using System;

public class WeaponSelect : CanvasLayer
{
    
    public String ChosenWeapon;

    // [Signal] public delegate void sig_WeaponChosen();


    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    // public void sig_SwordButtonPressed()
    // {
    //     ChosenWeapon = "sword";
    //     EmitSignal("sig_WeaponChosen", ChosenWeapon);
    //     QueueFree();
    // }

    // public void sig_AxeButtonPressed()
    // {
    //     ChosenWeapon = "axe";
    //     EmitSignal("sig_WeaponChosen", ChosenWeapon);
    //     QueueFree();
    // }

    // public void sig_HammerButtonPressed()
    // {
    //     ChosenWeapon = "hammer";
    //     EmitSignal("sig_WeaponChosen", ChosenWeapon);
    //     QueueFree();
    // }
}
