using Godot;
using System.Linq;

public partial class ControlsLine : Line2D
{
    [ExportGroup("Internal")]
    [Export]
    private PackedScene _pointScene;

    [Export]
    private Line2D _outline;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (Vector2 point in new Vector2[] { Points.First(), Points.Last() })
        {
            Node2D ponitInstance = _pointScene.Instantiate<Node2D>();
            AddChild(ponitInstance);
            ponitInstance.Position = point;
        }
        _outline.Points = Points;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
