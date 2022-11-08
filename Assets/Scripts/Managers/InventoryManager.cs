using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager
{
    // ½½·Ô¹øÈ£ 1~16
    public Dictionary<Define.Equipment, Equipment> Items { get; } = new Dictionary<Define.Equipment, Equipment>();
    public Equipment SelectedItem { get; set; } = null;

    public void Add(Equipment item)
    {
        Items.Add((Define.Equipment)item.Id, item);
    }

    public Equipment Get(Define.Equipment id)
    {
        Equipment item = null;
        Items.TryGetValue(id, out item);
        return item;
    }

    public Equipment Find(Func<Equipment, bool> condition)
    {
        foreach (Equipment item in Items.Values)
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
