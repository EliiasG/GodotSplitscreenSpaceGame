using Godot;

public partial class WinScreen : Control
{
    public Player Winner { get; set; }

    [Export]
    public GameData GameData { get; set; }

    [Export]
    private string _player1WinText;

    [Export]
    private string _player2WinText;

    [Export]
    private PackedScene _mainMenu;

    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private Label _winLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer.Play("appear");
        _winLabel.Text = Winner == GameData.Player1 ? _player1WinText : _player2WinText;
        _winLabel.Modulate = Winner?.ProjectileColor ?? new Color(1, 1, 1, 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept") || Winner == null)
        {
            _animationPlayer.Play("disappear");
        }
    }

    public void Finished()
    {
        GetParent().AddChild(_mainMenu.Instantiate());
        QueueFree();
    }
}
