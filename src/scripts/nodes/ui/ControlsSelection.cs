using Godot;

public partial class ControlsSelection : Control
{


    [ExportGroup("Internal")]
    [Export]
    private PackedScene _mainMenu;

    [Export]
    private NodePath[] _buttons = new NodePath[0];

    [Export]
    private AnimationPlayer _animationPlayer;

    private bool _isSelecting = true;
    private bool _done = false;
    private int _selectedIndex = 0;

    public bool IsSelecting
    {
        get => _isSelecting;
        set
        {
            if (_isSelecting == value) return;
            _isSelecting = value;

            if (_isSelecting)
            {
                _animationPlayer.Play("right");
            }
            else
            {
                _animationPlayer.Play("left");
            }
        }
    }

    private ControlsButton SelectedButton => (ControlsButton)GetNode(_buttons[_selectedIndex]);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SelectedButton.Selected();
        _animationPlayer.Play("start");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            IsSelecting = true;
        }

        if (!IsSelecting)
        {
            if (Input.IsActionJustPressed("ui_accept") && !_done)
            {
                _animationPlayer.Play("done");
                _done = true;
            }
            return;
        }

        if (Input.IsActionJustPressed("ui_left"))
        {
            SelectedButton.Deselected();
            _selectedIndex = Mathf.PosMod(_selectedIndex - 1, _buttons.Length);
            SelectedButton.Selected();
        }

        if (Input.IsActionJustPressed("ui_right"))
        {
            SelectedButton.Deselected();
            _selectedIndex = Mathf.PosMod(_selectedIndex + 1, _buttons.Length);
            SelectedButton.Selected();
        }

        if (Input.IsActionJustPressed("ui_accept"))
        {
            SelectedButton.Pressed();
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseEvent) return;

        mouseEvent.Pressed = false;
    }

    public void Finished()
    {
        GetParent().AddChild(_mainMenu.Instantiate());
        QueueFree();
    }
}
