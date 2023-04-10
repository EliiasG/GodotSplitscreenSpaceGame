using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayMenu : Control
{
    [Export]
    public GameModeInfo[] GameModes { get; private set; }

    [Export]
    public GameSessionDescription GameSessionDescription { get; private set; }

    [ExportGroup("Internal")]
    [Export]
    private Label _modeLabel;

    [Export]
    private Label _configurationModeLabel;

    [Export]
    private Label _descriptionLabel;

    [Export]
    private Control _settings;

    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private PackedScene _mainMenu;

    [Export]
    private PackedScene _playMenuSetting;

    private int _selectedModeIndex = 0;

    private bool _isConfiguring = false;

    private static readonly PropertyOptions<uint> _levelSizes = new("Level Size", new (string, uint)[]
    {
        ("Tiny", 2500u),
        ("Small", 5000u),
        ("Medium", 10000u),
        ("Large", 15000u),
        ("Huge", 20000u),
    });

    private static readonly PropertyOptions<float> _asteroidDensity = new("Asteroid Density", new (string, float)[]
    {
        ("None", -1),
        ("Scarce", 1500),
        ("Medium", 750),
        ("Dense", 450),
    });

    private GameModeInfo SelectedMode => GameModes[_selectedModeIndex];

    private IEnumerable<IPropertyOptions> Options => new IPropertyOptions[] { _levelSizes, _asteroidDensity }.Concat(SelectedMode.PropertyOptions);

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
        if (Input.IsActionJustPressed("ui_accept"))
        {
            if (_isConfiguring)
            {
                SelectedMode.Apply();
                GameSessionDescription.GameMode = SelectedMode.GameMode;
                LevelData data = GameSessionDescription.LevelDescription.Data;
                data.Width = _levelSizes.SelectedValue;
                data.Height = _levelSizes.SelectedValue;
                data.AsteroidDensity = _asteroidDensity.SelectedValue;
                _animationPlayer.Play("disappear");
            }
            else
            {
                _animationPlayer.Play("up");
                _isConfiguring = true;
                SetSettings();
            }
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

        if (_isConfiguring) return;

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
    }

    private void SetText()
    {
        _modeLabel.Text = SelectedMode.Name;
        _configurationModeLabel.Text = SelectedMode.Name;
        _descriptionLabel.Text = SelectedMode.Description;
    }

    private void SetSettings()
    {
        foreach (Node child in _settings.GetChildren())
        {
            if (child is not PlayMenuSetting) continue;
            _settings.RemoveChild(child);
        }
        PlayMenuSetting first = null;
        PlayMenuSetting prev = null;

        foreach (IPropertyOptions options in Options)
        {
            PlayMenuSetting setting = _playMenuSetting.Instantiate<PlayMenuSetting>();
            setting.Options = options;

            if (prev != null)
            {
                setting.FocusNeighborTop = prev.GetPath();
                prev.FocusNeighborBottom = setting.GetPath();
            }

            first ??= setting;
            prev = setting;

            _settings.AddChild(setting);
        }

        first.FocusNeighborTop = prev.GetPath();
        prev.FocusNeighborBottom = first.GetPath();

        first.GrabFocus();
    }

    public void Finished()
    {
        if (_isConfiguring)
        {
            GetParent<GameMenu>().Play(GameSessionDescription);
        }
        else
        {
            GetParent().AddChild(_mainMenu.Instantiate());
            QueueFree();
        }
    }
}
