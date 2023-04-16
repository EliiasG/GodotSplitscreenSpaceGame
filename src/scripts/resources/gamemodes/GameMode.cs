using Godot;

public abstract partial class GameMode : Resource
{
    [Export]
    private PackedScene _gameModeView { get; set; }

    public GameSession Session { get; set; }

    public GameModeView GenerateView(GameSession gameSession)
    {
        GameModeView view = _gameModeView.Instantiate<GameModeView>();
        view.Session = gameSession;
        PrepareView(view);
        return view;
    }

    public virtual void PrepareView(GameModeView view) { }

    public virtual void ShipCollidedWithAsteroid(PlayerShip ship, Node2D asteroid)
    {
        ship.Velocity = asteroid.GlobalPosition.DirectionTo(ship.GlobalPosition) * ship.Velocity.Length() * .5f;
    }

    public virtual void HitShip(PlayerShip ship, int damage)
    {
        ship.Explode();
        Session.End(Session.GameData.OtherPlayer(ship.Player));
    }

    public virtual void Begin()
    {
    }

    public virtual void Update(double delta)
    {
    }

    public virtual void ShipDied(PlayerShip ship)
    {

    }
}
