using Godot;

public partial class Pickup : Area2D
{
    [Export]
    public Texture2D Texture { get; set; }

    [Export]
    public GameData GameData { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private Sprite2D _sprite { get; set; }

    [Export]
    private AnimationPlayer _animationPlayer { get; set; }

    [Export]
    private Mappable _mapper { get; set; }

    public delegate void CollectedHandler(PlayerShip ship);

    public event CollectedHandler Collected;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BodyEntered += (Node2D body) =>
        {
            if (body is PlayerShip ship)
            {
                _animationPlayer.Play("collect");
                Collected?.Invoke(ship);
            }
        };
        _sprite.Texture = Texture;
        _mapper.GameData = GameData;
    }
}
