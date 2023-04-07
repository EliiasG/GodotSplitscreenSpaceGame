using Godot;

public partial class MainMenuButton : Label
{
    [Export]
    public Color SelectedColor { get; set; }

    [Export]
    public MainMenu MainMenu { get; set; }

    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string ScenePath { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private AnimationPlayer _animationPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FocusEntered += () =>
        {
            AddThemeColorOverride("font_color", SelectedColor);
            _animationPlayer.Play("select");
        };
        FocusExited += () =>
        {
            RemoveThemeColorOverride("font_color");
            _animationPlayer.Play("deselect");
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!HasFocus()) return;

        if (!Input.IsActionJustPressed("ui_accept")) return;

        MainMenu.ChangeScene(GD.Load<PackedScene>(ScenePath));
    }
}
