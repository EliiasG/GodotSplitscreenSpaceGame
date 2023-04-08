using Godot;

public partial class PlayMenu : Control
{
    [Export]
    public GameModeInfo[] GameModes { get; private set; }

    [Export]
    private Label _modeLabel;

    [Export]
    private Label _configurationModeLabel;

    [Export]
    private Label _descriptionLabel;

    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private PackedScene _mainMenu;

    private int _selectedModeIndex = 0;

    private bool _isConfiguring = false;

    private GameModeInfo SelectedMode => GameModes[_selectedModeIndex];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetText();
        _animationPlayer.Play("RESET");
        _animationPlayer.Play("appear");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_left"))
        {
            _animationPlayer.Play("previous");
            _selectedModeIndex = Mathf.PosMod(_selectedModeIndex - 1, GameModes.Length);
            SetText();
        }
        if (Input.IsActionJustPressed("ui_right"))
        {
            _animationPlayer.Play("next");
            _selectedModeIndex = Mathf.PosMod(_selectedModeIndex + 1, GameModes.Length);
            SetText();
        }
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (_isConfiguring)
            {
                _isConfiguring = false;
                _animationPlayer.Play("down");
            }
            else
            {
                _animationPlayer.Play("disappear");
            }
        }
        if (Input.IsActionJustPressed("ui_accept") && !_isConfiguring)
        {
            _animationPlayer.Play("up");
            _isConfiguring = true;
        }
    }

    private void SetText()
    {
        _modeLabel.Text = SelectedMode.Name;
        _configurationModeLabel.Text = SelectedMode.Name;
        _descriptionLabel.Text = SelectedMode.Description;
    }

    public void Finished()
    {
        GetParent().AddChild(_mainMenu.Instantiate());
        QueueFree();
    }
}
