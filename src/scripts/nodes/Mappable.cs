using Godot;

public partial class Mappable : Node2D
{
    private bool _mapped;

    private Texture2D _texture;

    [Export]
    public GameData GameData { get; set; }

    [Export]
    public Texture2D Texture { get; set; }

    [Export]
    public float MapSize { get; set; } = 1f;

    [Export]
    public int Layer { get; set; }

    [Export]
    public bool RotateView { get; set; }

    public override void _EnterTree()
    {
        CallDeferred("PlaceOnMap");
    }

    public override void _ExitTree()
    {
        RemoveFromMap();
    }

    public override void _Ready()
    {
        CallDeferred("PlaceOnMap");
    }

    private void PlaceOnMap()
    {
        if (_mapped) return;

        if (GameData == null)
        {
            GD.PushError("GameData is not set on '" + GetParent().Name + "/" + Name + "'");
        }

        GameData.Map.AddItem(this, Layer);

        _mapped = true;
    }

    private void RemoveFromMap()
    {
        GameData.Map.RemoveItem(this);
        _mapped = false;
    }
}
