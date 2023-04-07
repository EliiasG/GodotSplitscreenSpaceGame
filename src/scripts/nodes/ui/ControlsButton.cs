using Godot;

public partial class ControlsButton : Control
{
    [Export]
    public ControlsSelection ControlsSelection { get; set; }

    [Export]
    public Control ControlView { get; set; }

    [Export]
    public GameData GameData { get; set; }

    [Export]
    public PlayerControls Player1Controls { get; set; }

    [Export]
    public PlayerControls Player2Controls { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private AnimationPlayer _animationPlayer;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //not sure why this is neccesary, but without it they have the wrong size
        _animationPlayer.Play("RESET");
    }

    public void Selected()
    {
        if (!ControlsSelection.IsSelecting) return;
        ControlView.Visible = true;
        _animationPlayer.Play("select");
    }

    public void Deselected()
    {
        if (!ControlsSelection.IsSelecting) return;
        ControlView.Visible = false;
        _animationPlayer.Play("deselect");
    }

    public void Pressed()
    {
        if (!ControlsSelection.IsSelecting) return;
        _animationPlayer.Play("press");
        ControlsSelection.IsSelecting = false;
        GameData.Player1.Controls = Player1Controls;
        GameData.Player2.Controls = Player2Controls;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //FocusMode = ControlsSelection.IsSelecting ? FocusModeEnum.All : FocusModeEnum.None;
    }
}
