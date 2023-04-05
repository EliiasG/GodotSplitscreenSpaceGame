using Godot;

public partial class GameSessionRunner : Node
{
    private GameSession _session { get; set; }

    [Export]
    public GameSessionDescription SessionDescription { get; private set; }

    public override void _Ready()
    {
        _session = SessionDescription.GenerateSession();

        _session.GameData.Map.Level = _session.Level.Data;

        AddChild(_session.Mode.GenerateView(_session));

        _session.Mode.Begin();
        _session.Finished += (Player player) =>
        {
            GD.Print((player == _session.GameData.Player1 ? "Player 1" : "Player 2") + " won!");
            GetTree().Quit();
        };
    }

    public override void _Process(double delta)
    {
        _session.Mode.Update(delta);
    }
}
