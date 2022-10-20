using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StagePopup : UI_Popup
{
    // TODO : 데이터시트에서 받는걸로
    public StageLevels StageLevel { get; protected set; } = StageLevels.None;
    public int MonsterCount { get; protected set; }

    public enum StageLevels
    {
        None,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }

    protected enum Buttons
    {
        SkillButton1,
        SkillButton2,
        SkillButton3,
        SkillButton4,
    }

    protected enum GameObjects
    {
        Wall,
    }

    protected enum Texts
    {
        HpText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    protected void CreateMonster()
    {
        for(int i=0; i<MonsterCount; i++)
        {
            Managers.UI.MakeSubItem<UI_StagePopup>(gameObject.transform, "Monster");
        }
    }
}
