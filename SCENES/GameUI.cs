using Godot;
using System;

public class GameUI : CanvasLayer
{
	Label nameInfo;
	Label healthInfo;
	Label staminaInfo;
	Label massInfo;
	Label heightInfo;
	Label strengthInfo;
	Label weaponInfo;
	Label weaponLengthInfo;
	Label weaponMassInfo;


	public override void _Ready()
	{
		nameInfo = GetNode<Label>("ColorRect/GridContainer/NameInfo");
		healthInfo = GetNode<Label>("ColorRect/GridContainer/HealthInfo");
		staminaInfo = GetNode<Label>("ColorRect/GridContainer/StaminaInfo");
		massInfo = GetNode<Label>("ColorRect/GridContainer/MassInfo");
		heightInfo = GetNode<Label>("ColorRect/GridContainer/HeightInfo");
		strengthInfo = GetNode<Label>("ColorRect/GridContainer/StrengthInfo");
		weaponInfo = GetNode<Label>("ColorRect/GridContainer/WeaponInfo");
		weaponLengthInfo = GetNode<Label>("ColorRect/GridContainer/WeaponLengthInfo");
		weaponMassInfo = GetNode<Label>("ColorRect/GridContainer/WeaponMassInfo");
	}

	
    public void SetValues(Entity e)
	{
		nameInfo.Text = "" + e.name;
		healthInfo.Text = "" + e.currenthp + " J";
		staminaInfo.Text = "" + e.stamina + " J";
		massInfo.Text = "" + e.mass + " kg";
		heightInfo.Text = "" + e.height + " m";
		strengthInfo.Text = "" + e.str + " N";
		weaponInfo.Text = "" + e.weaponName;
		weaponLengthInfo.Text = "" + e.weaponLength + " m";
		weaponMassInfo.Text = "" + e.weaponMass + " kg";
	}
}
