using Godot;

public partial class PlayMenu : Control
{
    [Export]
    public GameModeInfo[] GameModes { get; private set; }

    [Export]
    public Label ModeLabel { get; private set; }

    [Export]
    public AnimationPlayer AnimationPlayer { get; private set; }

    private int _selectedModeIndex = 0;

    private GameModeInfo SelectedMode => GameModes[_selectedModeIndex];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetText();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_left"))
        {
            AnimationPlayer.Play("left");
            _selectedModeIndex = Mathf.PosMod(_selectedModeIndex - 1, GameModes.Length);
            SetText();
        }
        if (Input.IsActionJustPressed("ui_right"))
        {
            AnimationPlayer.Play("right");
            _selectedModeIndex = Mathf.PosMod(_selectedModeIndex + 1, GameModes.Length);
            SetText();
        }
    }

    private void SetText()
    {
        ModeLabel.Text = SelectedMode.Name;
    }
}
