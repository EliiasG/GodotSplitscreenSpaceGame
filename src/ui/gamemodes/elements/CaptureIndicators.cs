using Godot;

public partial class CaptureIndicators : Control
{
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public CaptureHandler CaptureHandler { get; set; }

    [Export]
    private ValueBar _player1bar;
    [Export]
    private ValueBar _player2bar;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _player1bar.Value = (float)(CaptureHandler.GetTime(Player1) / CaptureHandler.CaptureTime);
        _player2bar.Value = (float)(CaptureHandler.GetTime(Player2) / CaptureHandler.CaptureTime);
    }
}
