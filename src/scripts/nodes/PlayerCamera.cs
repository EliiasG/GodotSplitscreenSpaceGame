using Godot;

public partial class PlayerCamera : Camera2D
{
    private const string TransformName = "CameraTransform";

    [Export]
    public Player Player { get; private set; }

    [Export]
    public float Distance { get; private set; } = 500f;

    [Export]
    public float Speed { get; private set; } = 15f;

    private Vector2 _oldVelocity;
    private Vector2 _targetPosition;
    private Vector2 _position;

    public override void _Process(double delta)
    {
        if (Player.Ship is not PlayerShip ship)
        {
            return;
        }

        RemoteTransform2D remoteTransform;

        if (ship.HasNode(TransformName))
        {
            remoteTransform = ship.GetNode<RemoteTransform2D>(TransformName);
        }
        else
        {
            remoteTransform = new();
            remoteTransform.Name = TransformName;
            ship.AddChild(remoteTransform);
        }

        remoteTransform.RemotePath = GetPath();

        Vector2 targetTargetPosition = (_oldVelocity - ship.Velocity) * (float)delta * Distance;
        _targetPosition = _targetPosition.MoveToward(targetTargetPosition, (float)delta * Speed * 5);
        _position = _position.MoveToward(_targetPosition, (float)delta * Speed * _position.DistanceTo(_targetPosition));
        remoteTransform.GlobalPosition = ship.GlobalPosition + _position;

        _oldVelocity = ship.Velocity;
    }
}
