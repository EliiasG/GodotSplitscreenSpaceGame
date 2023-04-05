using Godot;

public partial class GameData : Resource
{
    [Export]
    public Player Player1 { get; private set; }

    [Export]
    public Player Player2 { get; private set; }

    public Map Map { get; } = new Map();

    [Export]
    private PackedScene PlayerShip { get; set; }

    [Export]
    private PackedScene Pickup { get; set; }

    public PlayerShip GenerateShip(Player player, GameSession session)
    {
        PlayerShip ship = PlayerShip.Instantiate<PlayerShip>();
        ship.Player = player;
        ship.Session = session;
        return ship;
    }

    public Pickup GeneratePickup(Texture2D texture)
    {
        Pickup pickup = Pickup.Instantiate<Pickup>();
        pickup.Texture = texture;
        pickup.GameData = this;
        return pickup;
    }

    public Player OtherPlayer(Player player)
    {
        return player == Player1 ? Player2 : Player1;
    }
}
