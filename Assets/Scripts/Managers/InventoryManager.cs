using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager
{
    public Dictionary<int, BowData> Items { get; } = new Dictionary<int, BowData>();

    public void Add(BowData item)
    {
        Items.Add(item.Id, item);
    }

    public BowData Get(int itemDbId)
    {
        BowData item = null;
        Items.TryGetValue(itemDbId, out item);
        return item;
    }

    public BowData Find(Func<BowData, bool> condition)
    {
        foreach (BowData item in Items.Values)
        {
            if (condition.Invoke(item))
                return item;
        }

        return null;
    }

    public void Clear()
    {
        Items.Clear();
    }
}
