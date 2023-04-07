using Godot;

public abstract partial class GameModeInfo : Resource
{
    [Export]
    public GameMode GameMode { get; private set; }

    public abstract string Name { get; }

    public abstract string Description { get; }


}
