using Godot;

public partial class ScrollingStars : ParallaxBackground
{
    [Export]
    public Vector2 Speed { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        ScrollOffset += Speed * (float)delta;
    }
}
