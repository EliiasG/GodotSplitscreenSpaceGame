using Godot;

public partial class GravityAttractor : Node2D
{
    private const float Threshold = 0.75f;

    [Export]
    public Node AffectedParent { get; private set; }

    [Export]
    public float Strength { get; private set; } = 100f;

    public override void _Process(double delta)
    {
        foreach (Node node in AffectedParent.GetChildren())
        {
            if (node is not CharacterBody2D characterBody)
            {
                continue;
            }

            Vector2 pos = GlobalPosition;
            Vector2 otherPos = characterBody.GlobalPosition;

            float amount = Strength / pos.DistanceSquaredTo(otherPos) * 1000000f;

            if (amount > Threshold)
            {
                characterBody.Velocity += (pos - otherPos).Normalized() * amount * (float)delta;
            }
        }
    }
}
