using System.Collections;

namespace Backend.ColorHelpers;

internal class BrightnessAgnosticColor : IList<string>
{
    private readonly List<string> _item;
    
    internal BrightnessAgnosticColor(string webColor)
    {
        this._item = new List<string> { webColor };
    }
    
    // The idea is to desregard the index and always return the single item.
    public string this[int _]
    {
        get
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("There is no value for the color. Clear(), Remove(), or RemoveAt() has been called prior.");
            }

            return _item[0];
        }
        
        // If setter is invoked, it appends new value rather then overwriting the original one.
        set
        {
            this._item.Add(value);
        }
    }
    
    // Rest of the methods is the interface implementation. Not suppose to use them.
    public void Add(string item)
    {
        this._item.Add(item);
    }
    
    public bool IsReadOnly => ((ICollection<string>)this._item).IsReadOnly;
    
    public int Count => this._item.Count;
    
    public void Insert(int index, string item)
    {
       this._item.Insert(index, item);
    }
    
    public bool Remove(string item)
    {
        return this._item.Remove(item);
    }
    
    public void RemoveAt(int index)
    {
        this._item.RemoveAt(index);
    }
        
    public int IndexOf(string item)
    {
        return this._item.IndexOf(item);
    }
    
    public IEnumerator<string> GetEnumerator()
    {
        return this._item.GetEnumerator();
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    public void Clear()
    {
        this._item.Clear();
    }
    
    public bool Contains(string item)
    {
        return this._item.Contains(item);
    }
    
    public void CopyTo(string[] target, int index)
    {
        this._item.CopyTo(target, index);
    }
}