using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArrowData : ILoader<int, Arrow>
{
    public List<Arrow> arrows = new List<Arrow>();

    public Dictionary<int, Arrow> MakeDict()
    {
        Dictionary<int, Arrow> dict = new Dictionary<int, Arrow>();
        foreach (Arrow arrow in arrows)
            dict.Add(arrow.Level, arrow);
        return dict;
    }
}

[Serializable]
public class Arrow
{
    public int Level;
    public float Speed;
}
