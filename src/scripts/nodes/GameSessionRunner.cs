using Godot;

public partial class GameSessionRunner : Node
{
    private GameSession _session { get; set; }

    [Export]
    public GameSessionDescription SessionDescription { get; set; }

    [Export(PropertyHint.File, "*.tscn,*.scn")]
    private string _gameMenuPath { get; set; }

    [Export]
    private PackedScene _winScreen;

    private bool _begun = false;

    public override void _Ready()
    {
        _session = SessionDescription.GenerateSession();

        _session.GameData.Map.Level = _session.Level.Data;

        AddChild(_session.GameMode.GenerateView(_session));

        _session.Finished += (Player player) =>
        {
            Node gameMenu = GD.Load<PackedScene>(_gameMenuPath).Instantiate();
            WinScreen winScreen = _winScreen.Instantiate<WinScreen>();
            winScreen.Winner = player;
            GetParent().AddChild(gameMenu);
            gameMenu.AddChild(winScreen);
            QueueFree();
        };
    }

    public override void _Process(double delta)
    {
        _session.GameMode.Update(delta);
        if (!_begun)
        {
            _session.GameMode.CallDeferred("Begin");
            _begun = true;
        }
    }
}
