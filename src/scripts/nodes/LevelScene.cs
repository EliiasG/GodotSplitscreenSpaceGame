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
    public Node2D PickupLocationsParent { get; private set; }

    [Export]
    public Node2D ProjectilesParent { get; private set; }
}
