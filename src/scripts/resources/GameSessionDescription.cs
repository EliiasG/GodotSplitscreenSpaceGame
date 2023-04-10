using Godot;

public partial class GameSessionDescription : Resource
{
    [Export]
    public LevelDescription LevelDescription { get; set; }

    [Export]
    public GameMode GameMode { get; set; }

    [Export]
    public GameData GameData { get; set; }

    public GameSession GenerateSession()
    {
        return new GameSession(GameMode, LevelDescription.GenerateLevel(), GameData);
    }
}
