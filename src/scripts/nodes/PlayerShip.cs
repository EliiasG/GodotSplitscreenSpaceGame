using Godot;

public partial class PlayerShip : CharacterBody2D
{
	private AnimationPlayer _animationPlayer;
	private Mappable _mapper;
	private Weapon _weapon;
	private bool _wasHitting = false;

	[Export]
	public Player Player { get; set; }

	public GameSession Session { get; set; }

	public float SpawnTime { get; set; } = 1f;

	public Weapon Weapon
	{
		get => _weapon;
		set
		{
			if (_weapon != null)
			{
				_weapon.PlayerShip = null;
				_weapon.Firing = false;
				RemoveChild(_weapon);
			}
			_weapon = value;
			if (_weapon != null)
			{
				_weapon.PlayerShip = this;
				AddChild(_weapon);
			}
		}
	}

	[Export]
	public float RotationSpeed { get; set; } = 5f;

	[Export]
	public float BorderForce { get; set; } = 100f;

	[Export]
	public float MaxSpeed { get; set; } = 1000f;

	[Export]
	public float AccelerationAmount { get; set; } = 100f;

	[Export]
	public float FuelConsumption { get; set; } = 1f;

	[Export]
	public float FuelRegeneration { get; set; } = 1f;

	[Export]
	public Curve RotationCurve { get; private set; }

	[Export]
	public Curve ThrustCurve { get; private set; }

	[Export]
	public Curve FuelConsumptionCurve { get; private set; }

	[Export]
	public Curve FuelYieldCurve { get; private set; }

	[Export]
	public float AimAssistAngleDegrees { get; private set; } = 12.5f;

	[Export]
	public float AimAssistDistance { get; private set; } = 1000f;

	public float Fuel { get; set; } = 1;

	public bool CanMove { get; set; } = false;

	public bool ShowArrow { get; set; } = false;

	public float ThrustYield => FuelYieldCurve.Sample(Fuel);

	public float BaseThrust => CanMove ? ThrustCurve.Sample(Input.GetActionStrength(Player.Controls.Thrust)) : 0;

	public float Thrust => BaseThrust * ThrustYield;

	public override void _EnterTree()
	{
		Player.Ship = this;
	}

	public override void _ExitTree()
	{
		if (Player.Ship == this)
		{
			Player.Ship = null;
		}
	}

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.SpeedScale = 1f / SpawnTime;
		GetNode<Area2D>("Collider").BodyEntered += (Node2D body) =>
		{
			Session.Mode.ShipCollidedWithAsteroid(this, body);
		};
		GetNode<Sprite2D>("Sprite").Texture = Player.Texture;
		_mapper = GetNode<Mappable>("Mapper");
		_mapper.Texture = Player.MapTexture;
		_mapper.GameData = Session.GameData;
	}


	public override void _PhysicsProcess(double delta)
	{
		if (!CanMove) return;

		Vector2 velocity = Velocity;

		Vector2 inputDir = Input.GetVector(Player.Controls.Left, Player.Controls.Right, Player.Controls.Up, Player.Controls.Down, 0.15f);

		inputDir = ApplyAimAssist(inputDir);

		if (inputDir != Vector2.Zero)
		{
			float angle = GetAngleTo(GlobalPosition + inputDir.Rotated(Mathf.Pi / 2));

			float rotateAmount = (float)Mathf.Clamp(angle, -RotationSpeed * delta, RotationSpeed * delta);

			Rotation += rotateAmount * RotationCurve.Sample(Mathf.Abs(angle / Mathf.Pi));
		}

		float baseThrust = BaseThrust;

		if (baseThrust != 0)
		{
			Fuel = Mathf.Lerp(Fuel, 0, FuelConsumptionCurve.Sample(BaseThrust) * FuelConsumption * (float)delta);
		}
		else
		{
			Fuel = Mathf.Min(Fuel + FuelRegeneration * (float)delta, 1);
		}

		if (velocity.LengthSquared() > MaxSpeed * MaxSpeed)
		{
			velocity = velocity.Normalized() * MaxSpeed;
		}

		bool hitting = false;

		if (Mathf.Abs(Position.X) > Session.Level.Data.Width / 2)
		{
			velocity.X = Mathf.Abs(velocity.X) * -Mathf.Sign(Position.X);
			hitting = true;
		}

		if (Mathf.Abs(Position.Y) > Session.Level.Data.Height / 2)
		{
			velocity.Y = Mathf.Abs(velocity.Y) * -Mathf.Sign(Position.Y);
			hitting = true;
		}

		if (Weapon != null)
		{
			Weapon.Firing = Input.IsActionPressed(Player.Controls.Shoot);
		}

		if (hitting && !_wasHitting)
		{
			PlayBounce();
		}

		_wasHitting = hitting;

		//Position = new Vector2(Mathf.Clamp(Position.X, -Level.Width / 2, Level.Width / 2), Mathf.Clamp(Position.Y, -Level.Height / 2, Level.Height / 2));

		//velocity -= velocity * Thrust * (float)delta;
		velocity += Vector2.Up.Rotated(Transform.Rotation) * Thrust * AccelerationAmount * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Started()
	{
		CanMove = true;
	}

	public void DoneSpawning()
	{
		_animationPlayer.SpeedScale = 1.0f;
	}

	public void PlayBounce()
	{
		_animationPlayer.Play("bounce");
	}

	public void PlayHit()
	{
		_animationPlayer.Play("hit");
	}

	public void Explode()
	{
		if (CanMove == false)
		{
			return;
		}
		_mapper.QueueFree();
		CanMove = false;
		Player.Ship = null;

		_animationPlayer.Play("explode");
	}

	private Vector2 ApplyAimAssist(Vector2 stickDir)
	{
		if (stickDir == Vector2.Zero) return stickDir;

		if (Weapon == null) return stickDir;

		PlayerShip other = Session.GameData.OtherPlayer(Player).Ship;

		if (other == null) return stickDir;

		if (GlobalPosition.DistanceSquaredTo(other.GlobalPosition) > AimAssistDistance * AimAssistDistance) return stickDir;

		Vector2 shipDiff = (other.GlobalPosition - GlobalPosition);

		float shipAngle = shipDiff.Angle();

		float stickAngle = stickDir.Angle();

		float diff = stickAngle - shipAngle;

		float error = new Vector2(Mathf.Cos(diff), Mathf.Sin(diff)).Angle();

		if (Mathf.Min(Mathf.Abs(error), Mathf.Abs(error - Mathf.Pi)) > Mathf.DegToRad(AimAssistAngleDegrees) / 2) return stickDir;

		return shipDiff.Normalized();
	}
}
