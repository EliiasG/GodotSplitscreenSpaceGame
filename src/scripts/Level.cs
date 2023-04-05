public class Level
{
    public Level(LevelData data, LevelScene scene)
    {
        Data = data;
        Scene = scene;
    }

    public LevelData Data { get; }

    public LevelScene Scene { get; }
}
