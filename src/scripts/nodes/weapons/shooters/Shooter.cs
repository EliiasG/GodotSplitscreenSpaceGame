using Godot;

public abstract partial class Shooter : Node2D
{

    [Export]
    public Weapon Weapon { get; private set; }

    [Export]
    public Godot.Collections.Array<bool> Stages { get; private set; }

    [Export]
    private AnimationPlayer _animationPlayer;

    private int _fireStage = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Weapon.Fire += () =>
        {
            if (Stages == null || Stages.Count == 0)
            {
                PlayFire();
                return;
            }
            if (Stages[_fireStage])
            {
                PlayFire();
            }
            _fireStage = (_fireStage + 1) % Stages.Count;
        };
    }

    public abstract void Fire();

    private void PlayFire()
    {
        Fire();
        if (_animationPlayer != null)
        {
            _animationPlayer.Play("fire");
        }
    }
}
