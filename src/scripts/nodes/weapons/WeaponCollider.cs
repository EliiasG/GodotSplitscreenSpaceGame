using Godot;

public partial class WeaponCollider : Area2D
{
    [Export]
    public PlayerShip Ship { get; set; }
}
