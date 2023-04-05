using Godot;

public partial class ValueBarBackGround : TextureRect
{
    [Export]
    public ValueBar ValueBar { get; private set; }

    public override void _Ready()
    {
        Modulate = ValueBar.BackgroundColor;
    }
}
