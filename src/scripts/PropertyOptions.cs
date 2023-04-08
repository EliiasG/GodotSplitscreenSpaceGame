using Godot;
using System.Collections.Generic;
using System.Linq;

public class PropertyOptions<T> : IPropertyOptions
{
    public string Name { get; }

    public string SelectedName => _options[_selectedIndex].Item1;

    public T SelectedValue => _options[_selectedIndex].Item2;

    private int _selectedIndex = 0;

    private readonly (string, T)[] _options;

    public PropertyOptions(string name, IEnumerable<(string, T)> options)
    {
        _options = options.ToArray();
        Name = name;

    }

    public void SelectNext()
    {
        _selectedIndex = Mathf.PosMod(_selectedIndex + 1, _options.Length);
    }

    public void SelectPrevious()
    {
        _selectedIndex = Mathf.PosMod(_selectedIndex - 1, _options.Length);
    }

}
