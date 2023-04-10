using Godot;

public partial class LevelScene : Node2D
{
    [Export]
    public Node2D Player1SpawnPoint { get; private set; }

    [Export]
    public Node2D Player2SpawnPoint { get; private set; }

    [Export]
    public Node2D PlayerParent { get; private set; }

    [Export]
    public NodeSpawner PickupLocationsSpawner { get; private set; }

    [Export]
    public NodeSpawner AsteroidSpawner { get; private set; }

    [Export]
    public Node2D ProjectilesParent { get; private set; }

    public LevelData LevelData;

    public override void _Ready()
    {
        if (LevelData.AsteroidDensity != -1)
        {
            AsteroidSpawner.AmountX = (uint)(LevelData.Width / LevelData.AsteroidDensity);
            AsteroidSpawner.AmountY = (uint)(LevelData.Height / LevelData.AsteroidDensity);
        }
        else
        {
            AsteroidSpawner.AmountX = 0;
            AsteroidSpawner.AmountY = 0;
        }

        PickupLocationsSpawner.Height = LevelData.Height - 750;
        PickupLocationsSpawner.Width = LevelData.Width - 750;

        AsteroidSpawner.Height = LevelData.Height - 500;
        AsteroidSpawner.Width = LevelData.Width - 500;

        Player2SpawnPoint.GlobalPosition = new Vector2(LevelData.Width / 2 - 800, LevelData.Height / 2 - 800);
        Player1SpawnPoint.GlobalPosition = -Player2SpawnPoint.GlobalPosition;
    }
}
