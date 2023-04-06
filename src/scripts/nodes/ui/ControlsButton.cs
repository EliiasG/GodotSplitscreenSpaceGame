using Godot;

public partial class ControlsButton : Control
{
    [ExportCategory("Internal")]
    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private TextureButton _button;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FocusEntered += () =>
        {
            _animationPlayer.Play("select");
            _button.GrabFocus();
        };

        _button.FocusExited += () =>
        {
            _animationPlayer.Play("deselect");
            _button.GrabFocus();
        };

        _button.ButtonDown += () =>
        {
            _animationPlayer.Play("press");
            Input.StartJoyVibration(0, 1, 0, .15f);
        };

        //not sure why this is neccesary, but without it they have the wrong size
        _animationPlayer.Play("RESET");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
