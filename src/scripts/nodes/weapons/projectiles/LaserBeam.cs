using Godot;

public partial class LaserBeam : Node2D
{
    public Vector2 StartPosition { get; set; }

    public Vector2 EndPosition { get; set; }

    public Color Color { get; set; }

    [Export]
    private Line2D _line;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GlobalPosition = EndPosition;
        _line.SetPointPosition(0, StartPosition - EndPosition);
        Modulate = Color;
    }
}
