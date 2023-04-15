using Godot;

public partial class HealthBar : Node2D
{
    [Export]
    public float MoveSpeed { get; set; }

    [Export]
    public float MoveDistance { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private PlayerShip _playerShip;

    [Export]
    private Node2D _pivot;

    [Export]
    private ValueBar _bar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GlobalRotation = 0;
        //_pivot.Position = _pivot.Position.MoveToward(_playerShip.Acceleration * -MoveDistance, (float)delta * MoveSpeed);
    }
}
