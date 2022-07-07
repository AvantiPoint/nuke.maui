using System.Collections;

namespace AvantiPoint.Nuke.Maui.CI;

public class CIVariableCollection : IEnumerable
{
    private readonly List<object> _items = new();

    public bool Any() => _items.Any();

    public CIVariableCollection Add(CIVariable variable)
    {
        _items.Add(variable);
        return this;
    }

    public CIVariableCollection Add(CIVariableGroup group)
    {
        _items.Add(group);
        return this;
    }

    public CIVariableCollection Add(string group)
    {
        _items.Add(new CIVariableGroup { Name = group });
        return this;
    }

    public CIVariableCollection Add(string key, string value)
    {
        _items.Add(new CIVariable { Key = key, Value = value });
        return this;
    }

    public IEnumerator GetEnumerator() => _items.GetEnumerator();
}
