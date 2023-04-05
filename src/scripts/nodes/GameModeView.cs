using Godot;

public partial class GameModeView : Control
{
    [Export]
    public World World { get; private set; }

    public GameSession Session { get; set; }

    public override void _Ready()
    {
        World.Session = Session;
    }
}
