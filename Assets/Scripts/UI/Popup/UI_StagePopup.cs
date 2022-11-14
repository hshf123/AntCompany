using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StagePopup : UI_BaseStagePopup
{
    public StageLevels StageLevel { get; set; } = StageLevels.None;

    Stage _stageData;

    public enum StageLevels
    {
        None,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        if (Managers.Data.StageDict.TryGetValue((int)StageLevel, out _stageData) == false)
        {
            Debug.Log($"Failed to load {StageLevel} data");
            return false;
        }
        Managers.Game.MonsterCount = _stageData.MonsterCount;

        CreateMonster();

        return true;
    }

    void Update()
    {
        UpdateHp();
        UpdateMonsterCount();
    }

    void UpdateMonsterCount()
    {
        if (Managers.Game.Monsters.Count == 0)
            StageEnd();
    }

    protected void CreateMonster()
    {
        for (int i = 0; i < Managers.Game.MonsterCount; i++)
        {
            GameObject monster = Managers.Resource.Instantiate("Objects/Monster", gameObject.transform);
            MonsterController mc = monster.GetComponent<MonsterController>();
            Managers.Game.Monsters.Add(mc);
            mc.SetStage(gameObject);
        }
    }
}
