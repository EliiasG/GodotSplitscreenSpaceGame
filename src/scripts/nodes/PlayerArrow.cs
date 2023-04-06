using Godot;

public partial class PlayerArrow : Node2D
{
    [Export]
    public float FadeStart { get; set; } = 1000f;

    [Export]
    public float FadeEnd { get; set; } = 500f;

    [Export]
    private PlayerShip _ship;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetVisibilityLayerBit(_ship.Player == _ship.Session.GameData.Player1 ? 1u : 2u, true);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Player other = _ship.Session.GameData.OtherPlayer(_ship.Player);

        Visible = _ship.ShowArrow && other.Ship != null;
        if (!Visible) return;

        LookAt(other.Ship.GlobalPosition);

        float fade = Mathf.Remap(GlobalPosition.DistanceTo(other.Ship.GlobalPosition), FadeEnd, FadeStart, 0, 1);

        Modulate = other.ProjectileColor * new Color(1, 1, 1, Mathf.Clamp(fade, 0, 1));
    }
}
