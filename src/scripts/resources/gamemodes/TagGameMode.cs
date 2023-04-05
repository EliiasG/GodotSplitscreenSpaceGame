using Godot;

public partial class TagGameMode : GameMode
{
    [Export]
    public double WinTime { get; set; }

    [Export]
    public float ChaserAcceleration { get; private set; } = 1000f;

    [Export]
    public float ChaserMaxSpeed { get; private set; } = 1000f;

    [Export]
    private Texture2D _pickupTexture;

    [Export]
    private PackedScene _weapon;

    public CaptureHandler CaptureHandler;

    private float _originalFuelConsumption;
    private float _originalMaxSpeed;
    private float _originalAcceleration;

    public override void Begin()
    {
        PlayerShip ship1 = Session.SpawnPlayer(Session.GameData.Player1, 0.0001f);
        _originalFuelConsumption = ship1.FuelConsumption;
        _originalMaxSpeed = ship1.MaxSpeed;
        _originalAcceleration = ship1.AccelerationAmount;
        Session.SpawnPlayer(Session.GameData.Player2, 0.0001f);
        Session.SpawnPickup(_pickupTexture).Collected += (PlayerShip ship) =>
        {
            SetRunner(ship.Player);
        };
        CaptureHandler.PlayerWon += (Player player) =>
        {
            Session.End(player);
        };
    }

    public override void PrepareView(GameModeView view)
    {
        CaptureHandler = new(WinTime);
        if (view is not TagGameModeView tagView)
        {
            GD.PushError("Game mode view is not for tag");
            return;
        }
        tagView.TagGameMode = this;
    }

    public override void HitShip(PlayerShip ship, int damage)
    {
        ship.PlayHit();
        SetRunner(Session.GameData.OtherPlayer(ship.Player));
    }

    private void SetRunner(Player player)
    {
        Player other = Session.GameData.OtherPlayer(player);
        CaptureHandler.Holder = player;
        player.Ship.Weapon = null;
        other.Ship.Weapon ??= _weapon.Instantiate<Weapon>();
    }

    public override void Update(double delta)
    {
        CaptureHandler.Update(delta);
        Player runner = CaptureHandler.Holder;
        if (runner != null)
        {
            runner.Ship.FuelConsumption = _originalFuelConsumption;
            runner.Ship.AccelerationAmount = _originalAcceleration;
            runner.Ship.MaxSpeed = _originalMaxSpeed;
            Player chaser = Session.GameData.OtherPlayer(runner);
            chaser.Ship.FuelConsumption = 0;
            chaser.Ship.Fuel = 1;
            chaser.Ship.MaxSpeed = ChaserMaxSpeed;
            chaser.Ship.AccelerationAmount = ChaserAcceleration;
        }
    }

    public override void ShipCollidedWithAsteroid(PlayerShip ship, Node2D asteroid)
    { 
        ship.Velocity = Vector2.Zero;
    }
}