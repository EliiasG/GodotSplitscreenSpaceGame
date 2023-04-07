using Godot;

public partial class MainMenu : Control
{
    [Export]
    private AnimationPlayer _animationPlayer;

    private PackedScene _scene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer.Play("appear");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ChangeScene(PackedScene scene)
    {
        if (_scene != null) return;
        _animationPlayer.Play("disappear");
        _scene = scene;
    }

    public void Finished()
    {
        GetParent().AddChild(_scene.Instantiate());
        QueueFree();
    }
}
