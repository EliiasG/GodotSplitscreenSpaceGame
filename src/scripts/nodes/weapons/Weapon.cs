using Godot;
using System;

public partial class Weapon : Node2D
{
    [Export]
    public bool UseAmmo { get; private set; }

    [Export]
    public int MaxAmmo { get; private set; }

    [Export]
    public bool Repeat { get; private set; }

    [Export]
    public double ReloadTime { get; private set; }

    public PlayerShip PlayerShip { get; set; }

    public double RemaningReloadTime { get; private set; }

    public int RemaningAmmo { get; private set; }

    public bool Firing { get; set; }

    public event Action Fire;

    private bool _wasFiring = false;

    public override void _Ready()
    {
        RemaningAmmo = MaxAmmo;
    }

    public override void _Process(double delta)
    {
        if (RemaningReloadTime <= 0 && Firing)
        {
            if (!_wasFiring || Repeat)
            {
                RemaningReloadTime = ReloadTime;
                if (UseAmmo)
                {
                    --RemaningAmmo;
                }
                Fire?.Invoke();
            }
        }
        else if (RemaningReloadTime > 0)
        {
            RemaningReloadTime -= delta;
        }
        _wasFiring = Firing;

        if (UseAmmo && RemaningAmmo <= 0)
        {
            PlayerShip.Weapon = null;
        }
    }
}