using Godot;

public partial class HealthBars : Control
{
	[Export]
	public GameModeView GameModeView { get; private set; }

	[ExportGroup("Internal")]
	[Export]
	public ValueBar Player1HealthBar { get; private set; }

	[Export]
	public ValueBar Player2HealthBar { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PlayerShip ship1 =  GameModeView.Session.GameData.Player1.Ship;
		Player1HealthBar.Value = (ship1?.Health ?? 0) / (float)(ship1?.MaxHealth ?? 1);

		PlayerShip ship2 = GameModeView.Session.GameData.Player2.Ship;
		Player2HealthBar.Value = (ship2?.Health ?? 0) / (float)(ship2?.MaxHealth ?? 1);
	}
}
