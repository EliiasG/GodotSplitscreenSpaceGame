using Godot;

public partial class WorldSetter : SubViewport
{
    [Export]
    public SubViewport Other { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        World2D = Other.World2D;
    }

}
