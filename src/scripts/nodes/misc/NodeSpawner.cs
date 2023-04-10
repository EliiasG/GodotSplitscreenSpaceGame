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
    public bool UseSeed { get; set; } = true;

    [Export]
    public ulong Seed { get; set; } = 1;

    [Export]
    public float Randomness { get; set; } = 1f;

    [Export]
    public bool RandomlyRotate { get; set; } = true;

    [Export]
    public bool GenerateOnRun { get; set; } = false;

    [Export]
    public PackedScene[] Scenes { get; set; } = System.Array.Empty<PackedScene>();

    [Export]
    public NodeSpawner RunAfter { get; set; }

    [Export]
    public NodePath[] DistanceParents { get; set; }

    [Export]
    public float[] Distances { get; set; }

    [Export]
    private bool _generate
    {
        get => false;
        set
        {
            if (!Engine.IsEditorHint() || !value) return;

            if (RunAfter != null)
            {
                RunAfter._generate = true;
            }

            Generate();
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

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint()) return;

        if (!GenerateOnRun) return;

        //if RunAfter is not null and its GenerateOnRun is true, it is not done and we must wait 
        if (RunAfter?.GenerateOnRun ?? false) return;

        Generate();

        GenerateOnRun = false;
    }

    public void Generate()
    {
        Clear();

        if (UseSeed)
        {
            GD.Seed(Seed);
        }
        else
        {
            GD.Randomize();
        }

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

                if (!CanPlace(pos)) continue;

                Node2D node = Scenes[GD.Randi() % Scenes.Length].Instantiate<Node2D>();

                node.Name += "_" + x + "_" + y;

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

    private bool CanPlace(Vector2 position)
    {
        for (int i = 0; i < (DistanceParents?.Length ?? 0); i++)
        {
            float distanceSq = Distances[i];
            distanceSq *= distanceSq;

            foreach (Node child in GetNode(DistanceParents[i]).GetChildren())
            {
                if (child is not Node2D node2d) continue;
                if (position.DistanceSquaredTo(node2d.GlobalPosition) <= distanceSq) return false;
            }
        }

        return true;
    }
}
