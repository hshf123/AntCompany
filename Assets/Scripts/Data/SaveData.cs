using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Save save = new Save();
}

[Serializable]
public class Save
{
    public string Name;
    public int Level;
    public float Exp;
    public int Money;
}
