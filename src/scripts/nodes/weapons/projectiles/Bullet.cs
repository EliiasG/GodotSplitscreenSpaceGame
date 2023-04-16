using Godot;

public partial class Bullet : Node2D
{
    [Export]
    public float Speed { get; set; }

    [Export]
    public int Damage { get; set; }

    public PlayerShip Firer { get; set; }

    [ExportGroup("Internal")]
    [Export]
    private Area2D _collider;

    private Vector2 _originalSpeed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _originalSpeed = Firer.Velocity;
        Modulate = Firer.Player.ProjectileColor;
        _collider.AreaEntered += (Area2D area) =>
        {
            if (area is WeaponCollider weaponCollider)
            {
                if (weaponCollider.Ship == Firer) return;
                weaponCollider.Ship.Damage(Damage);
            }
            QueueFree();
        };

        _collider.BodyEntered += (Node2D body) => QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GlobalPosition += (Transform.BasisXform(Vector2.Up) * Speed + _originalSpeed) * (float)delta;
    }
}
