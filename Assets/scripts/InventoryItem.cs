using UnityEngine;

public class InventoryItem
{
    public int ID;
    public string Name;
    public int Value;

    public InventoryItem(string name)
    {
        this.Name = name;
        ID = Random.Range(0, 1000);
        Value = Random.Range(0, 10000);
    }

    public override string ToString()
    {
        return Name;
    }
}
