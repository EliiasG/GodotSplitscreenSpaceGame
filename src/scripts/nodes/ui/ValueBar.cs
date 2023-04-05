using Godot;

public partial class ValueBar : Control
{
    [Export]
    public Color BackgroundColor { get; private set; } = new Color(1, 1, 1);

    [Export]
    public Color BarColor { get; private set; } = new Color(1, 1, 1);

    [Export]
    public float Value { get; set; }
}
