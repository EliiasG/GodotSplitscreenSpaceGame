using Godot;

public partial class World : Node2D
{
    [Export]
    public PackedScene Border { get; private set; }

    [Export]
    public float BorderWidth { get; private set; } = 10000f;

    [Export]
    public float ExtraSize { get; private set; } = 0f;

    private GameSession _gameSession;

    public GameSession Session
    {
        get => _gameSession;
        set
        {
            if (_gameSession != null)
            {
                GD.PushError("Only set world once!");
                return;
            }
            _gameSession = value;
            AddChild(Session.Level.Scene);

            foreach (int i in new int[] { 1, -1 })
            {
                Sprite2D borderX = Border.Instantiate<Sprite2D>();
                AddChild(borderX);
                borderX.Position = new Vector2((_gameSession.Level.Data.Width / 2 + BorderWidth / 4 + ExtraSize) * i, 0);
                borderX.Scale = new Vector2(BorderWidth, _gameSession.Level.Data.Height * 3 + BorderWidth * 2) / borderX.Texture.GetSize() / 2;

                Sprite2D borderY = Border.Instantiate<Sprite2D>();
                AddChild(borderY);
                borderY.Position = new Vector2(0, (_gameSession.Level.Data.Height / 2 + BorderWidth / 4 + ExtraSize) * i);
                borderY.Scale = new Vector2(_gameSession.Level.Data.Width * 3 + BorderWidth * 2, BorderWidth) / borderY.Texture.GetSize() / 2;
            }

        }
    }
}
