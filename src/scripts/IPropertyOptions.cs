public interface IPropertyOptions
{
    string Name { get; }

    string SelectedName { get; }

    void SelectNext();

    void SelectPrevious();
}
