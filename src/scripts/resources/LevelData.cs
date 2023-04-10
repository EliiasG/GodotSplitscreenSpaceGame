using Godot;

public partial class LevelData : Resource
{
    [Export]
    public uint Width { get; set; }

    [Export]
    public uint Height { get; set; }

    [Export]
    public float AsteroidDensity { get; set; }
}
