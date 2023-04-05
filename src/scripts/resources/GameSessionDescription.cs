using Godot;

public partial class GameSessionDescription : Resource
{
    [Export]
    LevelDescription LevelDescription { get; set; }

    [Export]
    GameMode GameMode { get; set; }

    [Export]
    GameData GameData { get; set; }

    public GameSession GenerateSession()
    {
        return new GameSession(GameMode, LevelDescription.GenerateLevel(), GameData);
    }
}
