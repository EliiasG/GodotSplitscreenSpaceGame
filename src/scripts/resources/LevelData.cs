using Godot;

public partial class LevelData : Resource
{
    [Export]
    public uint Width { get; private set; }

    [Export]
    public uint Height { get; private set; }
}
