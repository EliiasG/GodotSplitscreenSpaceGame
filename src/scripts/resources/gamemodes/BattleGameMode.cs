using Godot;
using System;

public partial class BattleGameMode : GameMode, IWeaponEnabledGameMode, ILifeCountedGameMode
{
    public int Player1Lives => throw new NotImplementedException();

    public int Player2Lives => throw new NotImplementedException();

    public void PlayerPickedUpWeapon(Player player, PickupWeapon weapon)
    {
        throw new NotImplementedException();
    }
}
