using Godot;

public partial class PlayMenuSetting : Control
{
    [ExportGroup("Internal")]
    [Export]
    private AnimationPlayer _animationPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FocusEntered += () =>
        {
            _animationPlayer.Play("select");
        };
        FocusExited += () =>
        {
            _animationPlayer.Play("deselect");
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
