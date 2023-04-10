public partial class TagGameModeInfo : GameModeInfo
{
    private readonly PropertyOptions<float> _time = new("Timer duration", new (string, float)[]
    {
        ("30 seconds", 5),
        ("1 minute", 60),
        ("1 minute and 30 seconds", 90),
        ("2 minutes", 120),
        ("3 minutes", 180),
        ("4 minutes", 240),
        ("5 minutes", 300),
    });

    public override string Name => "Tag";

    public override string Description =>
        "At the start a pickup is placed\n" +
        "The first player to collect the pickup becomes the runner\n" +
        "The other player becomes the tagger\n" +
        "As long as a player is the runner, that player's timer will be active\n" +
        "The tagger must shoot the runner to become the runner\n" +
        "A player wins when their timer finishes\n" +
        "The tagger has incread acceleration and max speed, and does not use fuel";

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
