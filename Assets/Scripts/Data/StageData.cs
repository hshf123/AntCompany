using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData : ILoader<int, Stage>
{
    public List<Stage> stages = new List<Stage>();

    public Dictionary<int, Stage> MakeDict()
    {
        Dictionary<int, Stage> dict = new Dictionary<int, Stage>();
        foreach (Stage stage in stages)
            dict.Add(stage.StageLevel, stage);
        return dict;
    }
}

[Serializable]
public class Stage
{
    public int StageLevel;
    public int MonsterCount;
}
