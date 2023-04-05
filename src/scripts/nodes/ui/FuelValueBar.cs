using Godot;

public partial class FuelValueBar : Control
{
    [Export]
    public Player Player { get; set; }

    private ValueBar _valueBar { get; set; }

    public override void _Ready()
    {
        _valueBar = GetNode<ValueBar>("ValueBar");
    }

    public override void _Process(double delta)
    {
        _valueBar.Value = Player.Ship?.Fuel ?? 0;
    }
}
