using Godot;

public partial class GameMenu : Control
{

    [Export]
    private PackedScene _sessionRunner;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void Play(GameSessionDescription session)
    {
        GameSessionRunner gameSessionRunner = _sessionRunner.Instantiate<GameSessionRunner>();
        gameSessionRunner.SessionDescription = session;
        GetParent().AddChild(gameSessionRunner);
        GetParent().RemoveChild(this);
    }
}
