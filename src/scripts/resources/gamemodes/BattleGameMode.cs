using Godot;

public partial class BattleGameMode : GameMode, IWeaponEnabledGameMode, ILifeCountedGameMode
{
    [Export]
    public float PickupSpawnTime { get; set; }

    [Export]
    public int PickupAmount { get; set; }

    [Export]
    public int PlayerHealth { get; set; }

    [Export]
    public int PlayerLives { get; set; }

    [Export]
    private WeaponCollection _weaponCollection;

    public int Player1Lives { get; private set; }

    public int Player2Lives { get; private set; }

    private int _remaningPickups;

    private float _remaningPickupTime;

    private float _winTime = 0;

    private Player _winner = null;

    public void PlayerPickedUpWeapon(Player player, PickupWeapon weapon)
    {
        weapon.GiveToPlayer(player);
        ++_remaningPickups;
    }

    public override void PrepareView(GameModeView view)
    {
        _winTime = .5f;
        _winner = null;
    }

    public override void Begin()
    {
        Player1Lives = PlayerLives;
        Player2Lives = PlayerLives;
        _remaningPickups = PickupAmount;
        _remaningPickupTime = PickupSpawnTime;

        Session.SpawnPlayer(Session.GameData.Player1, 0.001f, PlayerHealth);
        Session.SpawnPlayer(Session.GameData.Player2, 0.001f, PlayerHealth);
    }

    public override void Update(double delta)
    {
        if (_remaningPickups > 0)
        {
            _remaningPickupTime -= (float)delta;
            if (_remaningPickupTime < 0)
            {
                --_remaningPickups;
                _remaningPickupTime = PickupSpawnTime;
                _weaponCollection.GetRandom().SpawnPickup(Session);
            }
        }


        if (_winner != null)
        {
            _winTime -= (float)delta;
            if (_winTime <= 0)
            {
                Session.End(_winner);
            }
        }
    }

    public override void HitShip(PlayerShip ship, int damage)
    {
        ship.Damage(damage);
    }

    public override void ShipCollidedWithAsteroid(PlayerShip ship, Node2D asteroid)
    {
        ship.Velocity = Vector2.Zero;
    }

    public override void ShipDied(PlayerShip ship)
    {
        ship.Explode();

        if (ship.Player == Session.GameData.Player1)
        {
            --Player1Lives;
            if (Player1Lives == 0)
            {
                _winner = Session.GameData.Player2;
            }
        }
        else
        {
            --Player2Lives;
            if (Player2Lives == 0)
            {
                _winner = Session.GameData.Player1;
            }
        }

        PlayerShip other = Session.GameData.OtherPlayer(ship.Player).Ship;
        if (other != null)
        {
            other.Weapon = null;
            other.Regenerate(PlayerHealth / 4);
        }

        Session.SpawnPlayer(ship.Player, 1f, PlayerHealth);
    }
}
