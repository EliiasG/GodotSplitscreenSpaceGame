using Godot;

public partial class ReloadValueBar : CenterContainer
{
    [Export]
    public Player Player { get; set; }

    private ValueBar _reloadValueBar { get; set; }
    private ValueBar _ammoValueBar { get; set; }

    public override void _Ready()
    {
        _reloadValueBar = GetNode<ValueBar>("ReloadValueBar");
        _ammoValueBar = GetNode<ValueBar>("AmmoValueBar");
    }

    public override void _Process(double delta)
    {
        Weapon weapon = Player.Ship?.Weapon;
        //syntax at it's limit
        //maybe not the most beautiful code
        _reloadValueBar.Value = (float?)(weapon?.RemaningReloadTime / weapon?.ReloadTime) ?? -1;
        _ammoValueBar.Value = weapon?.UseAmmo ?? false ? weapon.RemaningAmmo / (float)weapon.MaxAmmo : -1;
    }
}
