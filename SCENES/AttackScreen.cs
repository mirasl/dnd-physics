using Godot;
using System;

public class AttackScreen : CanvasLayer
{
	const float GRAVITY = 9.8f;

	PackedScene popTextScene = GD.Load<PackedScene>("res://SCENES/PopText.tscn");
	Timer timer;
	ColorRect attackSelect;
	ColorRect colorRect;

	Entity attacker;
	Entity recipient;
	int distance;
	AnimatedSprite animatedSprite;


	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		attackSelect = GetNode<ColorRect>("AttackSelect");
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		colorRect = GetNode<ColorRect>("ColorRect");

		animatedSprite.Hide();
		colorRect.Color = new Color("a86adb");
	}

	public void PrepBattle(Entity attacker, Entity recipient, int distance)
	{
		attackSelect.Show();
		this.attacker = attacker;
		this.recipient = recipient;
		this.distance = distance;
	}

    public async void Battle(Entity attacker, Entity recipient, Level.Direction direction, int distance)
    {
		timer.Start();
		float torque = (attacker.weaponLength + attacker.height/2) * attacker.str;
		float work = torque * Mathf.Pi/2;
		float damage = 0;

		animatedSprite.Show();
		string animationName = attacker.weaponName;

        if (direction == Level.Direction.Side) 
		{
            damage = ((attacker.weaponLength + attacker.height/2) * (Mathf.Pi / 2) * (attacker.str));
			animationName += "Up";
        } 
		else if (direction == Level.Direction.Top) 
		{
			damage = ((attacker.weaponLength + attacker.height/2) * (Mathf.Pi / 2) * (attacker.str) + (attacker.weaponMass * GRAVITY * (attacker.height/2)));
            animationName += "Down";
        } 
		else if (direction == Level.Direction.Bottom) 
		{
			damage = ((attacker.weaponLength + attacker.height/2) * (Mathf.Pi / 2) * (attacker.str) - (attacker.weaponMass * GRAVITY * (attacker.height/2)));
            animationName += "Up";
        } 
		else if (direction == Level.Direction.Stab) 
		{
		   damage = distance * attacker.str;
           animationName += "Stab";
        }

		animatedSprite.Play(animationName);

		if (direction != Level.Direction.Stab)
		{
			PopText("TORQUE:");
			await ToSignal(timer, "timeout");

			PopText(attacker.weaponLength + " m + " + attacker.height/2 + " m");
			await ToSignal(timer, "timeout");

			PopText("X");
			await ToSignal(timer, "timeout");

			PopText(attacker.str + " N");
			await ToSignal(timer, "timeout");

			PopText("ROTATIONAL WORK:");
			await ToSignal(timer, "timeout");

			PopText(torque + " Nm");
			await ToSignal(timer, "timeout");

			PopText("X");
			await ToSignal(timer, "timeout");

			PopText("pi/2 rad");
			await ToSignal(timer, "timeout");

			if (direction == Level.Direction.Top)
			{
				PopText("+");
				await ToSignal(timer, "timeout");
			}
			else if (direction == Level.Direction.Bottom)
			{
				PopText("-");
				await ToSignal(timer, "timeout");
			}

			if (direction == Level.Direction.Top || direction == Level.Direction.Bottom)
			{
				PopText("GRAVITY:");
				await ToSignal(timer, "timeout");

				PopText(attacker.weaponMass + " kg");
				await ToSignal(timer, "timeout");
				
				PopText("X");
				await ToSignal(timer, "timeout");
				
				PopText("" + attacker.height/2 + " m");
				await ToSignal(timer, "timeout");

				PopText("X");
				await ToSignal(timer, "timeout");

				PopText("9.8 m/s/s");
				await ToSignal(timer, "timeout");
			}
		}
		else
		{
			PopText("WORK:");
			await ToSignal(timer, "timeout");

			PopText(distance + " m");
			await ToSignal(timer, "timeout");

			PopText("X");
			await ToSignal(timer, "timeout");

			PopText(attacker.str + " N");
			await ToSignal(timer, "timeout");

			PopText(distance + " m");
			await ToSignal(timer, "timeout");
		}

		PopText("=");
		await ToSignal(timer, "timeout");

		Label popText = popTextScene.Instance<Label>();
		popText.Text = damage + "";
		popText.SetPosition(new Vector2(50, 20));
		popText.RectScale = Vector2.One * 8;
		AddChild(popText);
		await ToSignal(timer, "timeout");

		Label popText2 = popTextScene.Instance<Label>();
		popText2.Text = "JOULES";
		popText2.SetPosition(new Vector2(90, 110));
		popText2.RectScale = Vector2.One * 4;
		AddChild(popText2);
		await ToSignal(timer, "timeout");

		PopText("of");
		await ToSignal(timer, "timeout");

		PopText("energy");
		await ToSignal(timer, "timeout");

		Label popText1 = popTextScene.Instance<Label>();
		popText1.Text = "OUCH!";
		popText1.SetPosition(new Vector2(40, 20));
		popText1.RectScale = Vector2.One * 8;
		AddChild(popText1);

		colorRect.Color = Colors.Red;

		float time = 0;
		while (time < 10)
		{
			await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
			Offset = new Vector2(GD.Randf()*10 - 5, GD.Randf() * 10 - 5);
			time++;
		}

		Clear();

    	// float damage = 0;
        // if (choice == Direction.Side) 
		// {
        //     damage = (weapon.Length * (Mathf.Pi / 2) * (str));
        // } 
		// else if (choice == Direction.Top) 
		// {
        //     damage = (weapon.Length * (Mathf.Pi / 2) * (str) + (mass * GRAVITY * (height/2)));
        // } 
		// else if (choice == Direction.Bottom) 
		// {
        //     damage = (weapon.Length * (Mathf.Pi / 2) * (str) - (mass * GRAVITY * (height/2)));
        // } 
		// else if (choice == Direction.Stab) 
		// {
        //     damage = distance * str;
        // }
    }
	
	private void Clear()
	{
		animatedSprite.Hide();
		colorRect.Color = new Color("a86adb");
		Hide();
	}

	private void PopText(string text)
	{
		Label popText = popTextScene.Instance<Label>();
		popText.Text = text;
		popText.SetRotation(GD.Randf() - 0.5f);
		popText.SetPosition(new Vector2(GD.Randf()*200 + 50, GD.Randf()*100 + 30));
		popText.RectScale = Vector2.One * (GD.Randf() + 1);
		AddChild(popText);
	}

	public void sig_SidePressed()
	{
		attackSelect.Hide();
		Battle(attacker, recipient, Level.Direction.Side, distance);
	}

	public void sig_UpPressed()
	{
		attackSelect.Hide();
		Battle(attacker, recipient, Level.Direction.Bottom, distance);
	}

	public void sig_DownPressed()
	{
		attackSelect.Hide();
		Battle(attacker, recipient, Level.Direction.Top, distance);
	}

	public void sig_StabPressed()
	{
		attackSelect.Hide();
		Battle(attacker, recipient, Level.Direction.Stab, distance);
	}
}
