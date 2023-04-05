using Godot;

public partial class TagGameModeView : GameModeView
{
    [Export]
    public CaptureIndicators CaptureIndicators { get; set; }

    public TagGameMode TagGameMode { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        CaptureIndicators.CaptureHandler = TagGameMode.CaptureHandler;
        CaptureIndicators.Player1 = TagGameMode.Session.GameData.Player1;
        CaptureIndicators.Player2 = TagGameMode.Session.GameData.Player2;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
