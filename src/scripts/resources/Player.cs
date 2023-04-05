using Godot;

public partial class Player : Resource
{
    [Export]
    public Texture2D Texture { get; private set; }

    [Export]
    public Texture2D MapTexture { get; private set; }

    [Export]
    public Color ProjectileColor { get; private set; }

    [Export]
    public PlayerControls Controls { get; set; }

    public PlayerShip Ship { get; set; }

}
