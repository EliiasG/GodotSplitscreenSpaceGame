using Godot;

public partial class WeaponCollection : Resource
{
    [Export]
    public PickupWeapon[] Weapons { get; private set; }

    public PickupWeapon GetRandom()
    {
        GD.Randomize();
        return Weapons[GD.Randi() % Weapons.Length];
    }
}
