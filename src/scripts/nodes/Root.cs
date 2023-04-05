using Godot;

public partial class Root : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("fullscreen"))
        {
            DisplayServer.WindowMode windowMode = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen ? DisplayServer.WindowMode.Windowed : DisplayServer.WindowMode.Fullscreen;

            DisplayServer.WindowSetMode(windowMode);
        }
    }
}
