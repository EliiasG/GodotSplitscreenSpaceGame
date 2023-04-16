using Godot;

public partial class BulletShooter : Shooter
{
    // Called when the node enters the scene tree for the first time.
    [Export]
    private PackedScene _bulletScene;

    public override void Fire()
    {
        Bullet bullet = _bulletScene.Instantiate<Bullet>();

        bullet.Firer = Weapon.PlayerShip;

        Weapon.PlayerShip.Session.Level.Scene.ProjectilesParent.AddChild(bullet);

        bullet.GlobalPosition = GlobalPosition;
        bullet.GlobalRotation = GlobalRotation;
    }
}
