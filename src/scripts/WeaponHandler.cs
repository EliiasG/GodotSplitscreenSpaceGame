public partial class WeaponHandler
{
    public GameData GameData { get; }

    public PickupWeapon Player1Next { get; private set; }
    public PickupWeapon Player2Next { get; private set; }

    public PickupWeapon Player1Weapon { get; private set; }

    public PickupWeapon Player2Weapon { get; private set; }

    public WeaponHandler(GameData gameData)
    {
        GameData = gameData;
    }

    public void GivePlayerWeapon(Player player, PickupWeapon weapon)
    {
        if (player == GameData.Player1)
        {
            Player1Next = weapon;
        }
        else
        {
            Player2Next = weapon;
        }
    }

    public void Update()
    {
        if (GameData.Player1.Ship.Weapon == null)
        {
            Player1Next?.GiveToPlayer(GameData.Player1);
            Player1Weapon = Player1Next;
            Player1Next = null;
        }
        if (GameData.Player2.Ship.Weapon == null)
        {
            Player2Next?.GiveToPlayer(GameData.Player2);
            Player2Weapon = Player2Next;
            Player2Next = null;
        }
    }

    public void ResetPlayer(Player player)
    {
        if (player == GameData.Player1)
        {
            Player1Next = null;
        }
        else
        {
            Player2Next = null;
        }
    }

}
