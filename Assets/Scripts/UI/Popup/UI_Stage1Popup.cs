using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage1Popup : UI_StagePopup
{
    enum Buttons
    {
        SkillButton1,
        SkillButton2,
        SkillButton3,
        SkillButton4,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        MonsterCount = 1;

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.SkillButton1).gameObject.BindEvent(OnClickSkillButton1);

        CreateMonster();

        return true;
    }

    void OnClickSkillButton1()
    {

    }
    void OnClickSkillButton2()
    {

    }
    void OnClickSkillButton3()
    {

    }
    void OnClickSkillButton4()
    {

    }
}
