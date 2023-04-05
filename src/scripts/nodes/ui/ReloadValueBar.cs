using Godot;

public partial class ReloadValueBar : CenterContainer
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
        Weapon weapon = Player.Ship?.Weapon;
        _valueBar.Value = Mathf.Min(1 - (float?)(weapon?.RemaningReloadTime / weapon?.ReloadTime) ?? -1, 1);
    }
}
