using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class MapView : ColorRect
{
    [Export]
    public GameData GameData { get; private set; }

    private float rotation = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        QueueRedraw();
        rotation += (float)delta * Mathf.Tau;
    }

    public override void _Draw()
    {
        List<int> layers = GameData.Map.Layers.Keys.ToList();
        layers.Sort();
        foreach (int layer in layers)
        {
            foreach (Mappable item in GameData.Map.Layers[layer])
            {
                DrawItem(item);
            }
        }

        //DrawCentered(Test, GetViewport().GetMousePosition() - GlobalPosition, rotation);
    }

    private void DrawItem(Mappable item)
    {
        Vector2 size = Vector2.One * Mathf.Max(GameData.Map.Level.Width, GameData.Map.Level.Height) / 2;

        Vector2 position = new(
            x: Mathf.Remap(item.GlobalPosition.X, -size.X, size.X, 0, Size.X),
            y: Mathf.Remap(item.GlobalPosition.Y, -size.Y, size.Y, 0, Size.Y)
        );

        DrawCentered(item.Texture, position, item.RotateView ? item.GlobalRotation : 0, item.MapSize);
    }

    private void DrawCentered(Texture2D texture, Vector2 position, float rotation = 0f, float size = 1f)
    {
        DrawSetTransform(position, rotation, new Vector2(size, size));
        DrawTexture(texture, -texture.GetSize() / 2);
    }

    //using clip contents instead
    /*
    private void DrawInside(Texture2D texture, Vector2 position)
    {
        Vector2 textureSize = texture.GetSize();

        if (position.X > Size.X || position.Y > Size.Y)
        {
            return;
        }

        if (position.X < -textureSize.X || position.Y < -textureSize.Y)
        {
            return;
        }

        Vector2 srcPos = new(Mathf.Max(-position.X, 0), Mathf.Max(-position.Y, 0));
        Vector2 overlap = Size - position;
        Vector2 drawSize = new Vector2(Mathf.Min(overlap.X, textureSize.X), Mathf.Min(overlap.Y, textureSize.Y)) - srcPos;
        Vector2 pos = new Vector2(Mathf.Clamp(position.X, 0, Size.X), Mathf.Clamp(position.Y, 0, Size.Y));
        DrawTextureRectRegion(texture, new Rect2(pos, drawSize), new Rect2(srcPos, drawSize));
    }
    */
}
