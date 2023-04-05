using Godot;

public partial class FireParticles : GpuParticles2D
{
    [Export]
    public PlayerShip Ship { get; private set; }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Scale = Vector2.One * Ship.Thrust;
    }
}
