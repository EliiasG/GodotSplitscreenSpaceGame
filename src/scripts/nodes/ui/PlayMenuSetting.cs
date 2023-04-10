using Godot;

public partial class PlayMenuSetting : Control
{
    public IPropertyOptions Options { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private Label _setting;

    [Export]
    private Label _value;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _setting.Text = Options.Name;
        _value.Text = Options.SelectedName;
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
        if (!HasFocus()) return;
        if (Input.IsActionJustPressed("ui_left"))
        {
            _animationPlayer.Play("previous");
            Options.SelectPrevious();
            _value.Text = Options.SelectedName;
        }
        if (Input.IsActionJustPressed("ui_right"))
        {
            _animationPlayer.Play("next");
            Options.SelectNext();
            _value.Text = Options.SelectedName;
        }
    }
}
