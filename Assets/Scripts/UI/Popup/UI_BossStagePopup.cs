using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossStagePopup : UI_BaseStagePopup
{
    BossController _boss;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreateBoss();

        return true;
    }

    void Update()
    {
        UpdateHp();
        UpdateBossStage();
    }
    
    void UpdateBossStage()
    {
        if (_boss.Hp == 0)
            StageEnd();
    }

    protected void CreateBoss()
    {
        GameObject boss = Managers.Resource.Instantiate("Objects/Boss", gameObject.transform);
        _boss = boss.GetComponent<BossController>();
        //Managers.Game.Monsters.Add(bc);
        _boss.SetStage(gameObject);
    }
}
