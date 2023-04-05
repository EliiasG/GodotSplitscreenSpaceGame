using System.Collections.Generic;

public partial class Map
{
    public LevelData Level { get; set; }

    public Dictionary<int, ISet<Mappable>> Layers { get; } = new();

    public void AddItem(Mappable item, int layer)
    {
        if (!Layers.TryGetValue(layer, out ISet<Mappable> nodes))
        {
            nodes = new HashSet<Mappable>();
            Layers.Add(layer, nodes);
        }

        nodes.Add(item);
    }

    public void RemoveItem(Mappable item)
    {
        foreach (ISet<Mappable> layer in Layers.Values)
        {
            layer.Remove(item);
        }
    }
}
