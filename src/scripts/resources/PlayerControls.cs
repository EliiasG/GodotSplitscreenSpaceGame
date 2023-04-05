using Godot;

public partial class PlayerControls : Resource
{
    [Export]
    public StringName Up { get; private set; } = "";

    [Export]
    public StringName Down { get; private set; } = "";

    [Export]
    public StringName Left { get; private set; } = "";

    [Export]
    public StringName Right { get; private set; } = "";

    [Export]
    public StringName Thrust { get; private set; } = "";

    [Export]
    public StringName Shoot { get; private set; } = "";
}
