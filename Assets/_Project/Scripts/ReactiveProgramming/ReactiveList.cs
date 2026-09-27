using System;
using System.Collections.Generic;

public class ReactiveList<T>
{
    public event Action<T> Added;
    public event Action<T> Removed;

    private readonly List<T> _elements = new List<T>();

    public IReadOnlyList<T> Elements => _elements;

    public void Add(T element)
    {
        _elements.Add(element);

        Added?.Invoke(element);
    }

    public void Remove(T element)
    {
        _elements.Remove(element);

        Removed?.Invoke(element);
    }
}