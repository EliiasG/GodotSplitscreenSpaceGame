using Godot;

public partial class LevelDescription : Resource
{
    [Export]
    public PackedScene Scene { get; private set; }

    [Export]
    public LevelData Data { get; private set; }

    public Level GenerateLevel()
    {
        LevelScene scene = Scene.Instantiate<LevelScene>();
        scene.LevelData = Data;
        if (scene.PickupLocationsSpawner.GetChildCount() < 10)
        {
            GD.PushError("There must be at least 10 pickup locations");
        }
        return new Level(Data, scene);
    }
}
