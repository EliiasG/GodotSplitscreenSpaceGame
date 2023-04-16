public partial class BattleGameModeInfo : GameModeInfo
{
    private readonly PropertyOptions<int> _pickupAmount = new("Pickup amount", new (string, int)[]
    {
        ("1", 1),
        ("2", 2),
        ("3", 3),
        ("4", 4),
        ("5", 5),
    });

    private readonly PropertyOptions<int> _playerLives = new("Lives", new (string, int)[]
    {
        ("1", 1),
        ("2", 2),
        ("3", 3),
        ("5", 5),
        ("10", 5),
        ("15", 5),
        ("20", 5),
        ("25", 5),
    });

    private readonly PropertyOptions<int> _playerHealth = new("Health", new (string, int)[]
    {
        ("First hit kills", 1),
        ("25", 25),
        ("50", 50),
        ("100", 100),
        ("150", 150),
        ("200", 200),
        ("250", 250),
        ("500", 500),
    });

    public override string Name => "Battle";

    public override string Description =>
        "Both players have a specified amount of lives\n" +
        "When a player dies, they lose a life\n" +
        "When a player kills, they lose their weapon and regnerate some health\n" +
        "When a player runs out of lives they lose\n" +
        "To get weapons players must collect pickups\n";

    public override IPropertyOptions[] PropertyOptions { get; }

    public BattleGameModeInfo()
    {
        PropertyOptions = new IPropertyOptions[]
        {
            _pickupAmount,
            _playerLives,
            _playerHealth,
        };
    }

    public override void Apply()
    {
        BattleGameMode battleMode = (BattleGameMode)GameMode;

        battleMode.PickupAmount = _pickupAmount.SelectedValue;
        battleMode.PlayerLives = _playerLives.SelectedValue;
        battleMode.PlayerHealth = _playerHealth.SelectedValue;
    }
}
