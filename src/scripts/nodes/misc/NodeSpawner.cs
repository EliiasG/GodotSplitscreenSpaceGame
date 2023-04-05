using Godot;

[Tool]
public partial class NodeSpawner : Node
{
    [Export]
    public float Width { get; set; } = 1000;

    [Export]
    public float Height { get; set; } = 1000;

    [Export]
    public uint AmountX { get; set; } = 100;

    [Export]
    public uint AmountY { get; set; } = 100;

    [Export]
    public bool Center { get; set; } = false;

    [Export]
    public ulong Seed { get; set; } = 1;

    [Export]
    public float Randomness { get; set; } = 1f;

    [Export]
    public bool RandomlyRotate { get; set; } = true;

    [Export]
    public PackedScene[] Scenes { get; set; } = new PackedScene[0];

    [Export]
    private bool _generate
    {
        get => false;
        set
        {
            if (!Engine.IsEditorHint() || !value)
            {
                return;
            }

            Clear();

            GD.Seed(Seed);

            float moveX = Width / AmountX;
            float moveY = Height / AmountY;
            for (int x = 0; x < AmountX; x++)
            {
                for (int y = 0; y < AmountX; y++)
                {
                    Vector2 pos = new Vector2(x * moveX + moveX / 2, y * moveY + moveY / 2);
                    if (Center)
                    {
                        pos.X -= Width / 2;
                        pos.Y -= Height / 2;
                    }
                    Vector2 random = new Vector2((GD.Randf() - 0.5f) * Randomness, (GD.Randf() - 0.5f) * Randomness);
                    pos += random * new Vector2(moveX, moveY);

                    Node2D node = Scenes[GD.Randi() % Scenes.Length].Instantiate<Node2D>();

                    node.Name += "_" + x + "_" + y;

                    //it is not done when this is called, so it must wait
                    AddChild(node);

                    node.Owner = GetTree().EditedSceneRoot;

                    node.Position = pos;
                    if (RandomlyRotate)
                    {
                        node.Rotation = GD.Randf() * Mathf.Tau;
                    }
                }
            }
        }
    }

    [Export]
    private bool _clear
    {
        get => false;
        set
        {
            if (Engine.IsEditorHint() && value)
            {
                Clear();
            }
        }
    }

    public void Clear()
    {
        foreach (Node child in GetChildren())
        {
            child.QueueFree();
        }
    }
}
