using Godot;

public partial class ValueBarBar : TextureRect
{
    [Export]
    public ValueBar ValueBar { get; private set; }

    public override void _Ready()
    {
        Modulate = ValueBar.BarColor;
    }

    public override void _Process(double delta)
    {
        Position = new Vector2(Mathf.Lerp(-192, -7, Mathf.Clamp(ValueBar.Value, 0, 1)), -.5f);
    }
}
