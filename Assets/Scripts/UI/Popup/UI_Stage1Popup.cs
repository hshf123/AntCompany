using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage1Popup : UI_StagePopup
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        StageLevel = StageLevels.Stage1;

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Get<Button>((int)Buttons.SkillButton1).gameObject.BindEvent(OnClickSkillButton1);
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(Managers.Game.HP);
        GetText((int)Texts.HpText).text = Managers.Game.HP.ToString();

        MonsterCount = 1;

        CreateMonster();

        return true;
    }

    void Update()
    {
        int hp = Managers.Game.HP;
        if(hp<=0)
        {
            hp = 0;
        }
        GetText((int)Texts.HpText).text = hp.ToString();
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
