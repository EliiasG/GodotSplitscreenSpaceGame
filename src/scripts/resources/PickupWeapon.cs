using Godot;

public partial class PickupWeapon : Resource
{
    [Export]
    public PackedScene WeaponScene { get; private set; }

    [Export]
    public Texture2D PickupTexture { get; private set; }

    public void SpawnPickup(GameSession session)
    {
        session.SpawnPickup(PickupTexture).Collected += (PlayerShip ship) =>
        {
            if (session.GameMode is not IWeaponEnabledGameMode gameMode)
            {
                GD.PushError("Mode does not allow pickup weapons");
                return;
            }

            gameMode.PlayerPickedUpWeapon(ship.Player, this);
        };
    }

    public void GiveToPlayer(Player player)
    {
        player.Ship.Weapon = WeaponScene.Instantiate<Weapon>();
    }
}
