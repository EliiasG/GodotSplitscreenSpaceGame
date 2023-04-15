using Godot;

public partial class LifeCounters : Control
{
	[Export]
	public GameModeView GameModeView { get; private set; }

	[ExportGroup("Internal")]
	[Export]
	public Label Player1LifeCounter { get; private set; }

	[Export]
	public Label Player2LifeCounter { get; private set; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameModeView.Session.GameMode is not ILifeCountedGameMode lifeCounted)
		{
			GD.PushError("Mode is not life counted");
			return;
		}

		Player1LifeCounter.Text = lifeCounted.Player1Lives.ToString();
		Player2LifeCounter.Text = lifeCounted.Player2Lives.ToString();
	}
}
