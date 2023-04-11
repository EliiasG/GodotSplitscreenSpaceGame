using Godot;

public partial class LaserShooter : Shooter
{
    [Export]
    public int Damage { get; private set; }

    [Export]
    private PackedScene _beam;

    [Export]
    private RayCast2D _rayCast;

    public override void Fire()
    {
        Vector2 hit = _rayCast.IsColliding() ? _rayCast.GetCollisionPoint() : _rayCast.ToGlobal(_rayCast.TargetPosition);
        LaserBeam laserBeam = _beam.Instantiate<LaserBeam>();
        laserBeam.StartPosition = GlobalPosition;
        laserBeam.EndPosition = hit;
        laserBeam.Color = Weapon.PlayerShip.Player.ProjectileColor;

        //beautiful
        Weapon.PlayerShip.Session.Level.Scene.ProjectilesParent.AddChild(laserBeam);

        //maybe .GetParent is needed? not sure if GetCollider() actually gets the collider or the area/body
        if (_rayCast.GetCollider() is WeaponCollider weaponCollider)
        {
            PlayerShip ship = weaponCollider.Ship;
            ship.Session.GameMode.HitShip(ship, Damage);
        }
    }
}
