using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameSession
{
    public GameSession(GameMode mode, Level level, GameData gameData)
    {
        GameMode = mode;
        Level = level;
        GameData = gameData;

        GameMode.Session = this;
    }

    public GameMode GameMode { get; }

    public Level Level { get; }

    public GameData GameData { get; }

    public event Action<Player> Finished;

    public PlayerShip SpawnPlayer(Player player, float spawnTime = 1f, int maxHealth = 100)
    {
        if (player.Ship != null)
        {
            GD.PushError("Tried to spawn player, but player already has a ship");
        }
        PlayerShip ship = GameData.GenerateShip(player, this);
        ship.SpawnTime = spawnTime;
        ship.MaxHealth = maxHealth;
        Level.Scene.PlayerParent.CallDeferred(Node.MethodName.AddChild, ship);
        Node2D spawnpoint = player == GameData.Player1 ? Level.Scene.Player1SpawnPoint : Level.Scene.Player2SpawnPoint;
        ship.GlobalPosition = spawnpoint.GlobalPosition;
        ship.GlobalRotation = spawnpoint.GlobalRotation;
        return ship;
    }

    public Pickup SpawnPickup(Texture2D texture)
    {
        Pickup pickup = GameData.GeneratePickup(texture);

        IEnumerable<Node> pickupLocations = Level.Scene.PickupLocationsSpawner.GetChildren();

        //get all spawn locations where there are no children
        Node[] avalible = pickupLocations.Where((Node node) => node.GetChildCount() == 0).ToArray();

        if (avalible.Length == 0)
        {
            GD.PushWarning("All pickup locations are occupied!");
            return pickup;
        }

        //in case modified elsewhere
        GD.Randomize();

        Node parent = avalible[GD.Randi() % avalible.Length];

        parent.AddChild(pickup);

        return pickup;
    }

    public void End(Player winner)
    {
        Finished?.Invoke(winner);
    }
}
