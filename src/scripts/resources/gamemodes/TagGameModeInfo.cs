using Godot;

public partial class TagGameModeInfo : GameModeInfo
{
    [Export]
    private string _name;

    [Export]
    private string _description;

    private readonly PropertyOptions<float> _time = new("Timer duration", new (string, float)[]
    {
        ("30 seconds", 30),
        ("1 minute", 60),
        ("1 minute and 30 seconds", 90),
        ("2 minutes", 120),
        ("3 minutes", 180),
        ("4 minutes", 240),
        ("5 minutes", 300),
    });

    public override string Name => _name;

    public override string Description => _description;

    public override IPropertyOptions[] PropertyOptions { get; }

    public TagGameModeInfo()
    {
        PropertyOptions = new IPropertyOptions[]
        {
            _time,
        };
    }

    public override void Apply()
    {
        TagGameMode tagMode = (TagGameMode)GameMode;

        tagMode.WinTime = _time.SelectedValue;
    }
}
