using Godot;

public partial class EmptyGameMode : GameMode
{
    [Export]
    private double _respawnTime;

    [Export]
    private Texture2D _pickupTexture;

    [Export]
    private PackedScene _weapon;

    private double? _player1RespawnTime;
    private double? _player2RespawnTime;

    public override void Begin()
    {
        Session.SpawnPlayer(Session.GameData.Player1, 0.001f);
        Session.SpawnPlayer(Session.GameData.Player2, 0.001f);
        for (int i = 0; i < 10; i++)
        {
            Pickup pickup = Session.SpawnPickup(_pickupTexture);
            pickup.Collected += (PlayerShip ship) =>
            {
                ship.Weapon ??= _weapon.Instantiate<Weapon>();
            };
        }
    }

    public override void HitShip(PlayerShip ship, int damage)
    {
        ship.PlayHit();
    }

    public override void ShipCollidedWithAsteroid(PlayerShip ship, Node2D asteroid)
    {
        base.ShipCollidedWithAsteroid(ship, asteroid);
        /*
        ship.Explode();
        if (ship.Player == Session.GameData.Player1)
        {
            _player1RespawnTime = _respawnTime;
        }
        else
        {
            _player2RespawnTime = _respawnTime;
        }
        */
    }

    public override void Update(double delta)
    {
        if (_player1RespawnTime != null)
        {
            _player1RespawnTime -= delta;
            if (_player1RespawnTime <= 0)
            {
                _player1RespawnTime = null;
                //Session.SpawnPlayer(Session.GameData.Player1, 1f);
                Session.End(Session.GameData.Player2);
            }
        }
        else if (Session.GameData.Player1.Ship == null)
        {
            _player1RespawnTime = _respawnTime;
        }

        if (_player2RespawnTime != null)
        {
            _player2RespawnTime -= delta;
            if (_player2RespawnTime <= 0)
            {
                _player2RespawnTime = null;
                //Session.SpawnPlayer(Session.GameData.Player2, 1f);
                Session.End(Session.GameData.Player1);
            }
        }
        else if (Session.GameData.Player2.Ship == null)
        {
            _player2RespawnTime = _respawnTime;
        }
    }
}
