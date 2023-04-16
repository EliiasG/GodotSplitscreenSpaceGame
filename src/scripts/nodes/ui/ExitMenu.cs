using Godot;

public partial class ExitMenu : Control
{
    [Export]
    private GameModeView _gameModeView;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        bool paused = GetTree().Paused;
        Visible = paused;
        if (Input.IsActionJustPressed("ui_cancel")) GetTree().Paused = !paused;

        if (!paused) return;

        if (Input.IsActionJustPressed("ui_accept"))
        {
            _gameModeView.World.Session.End(null);
            GetTree().Paused = false;
        }
    }
}
