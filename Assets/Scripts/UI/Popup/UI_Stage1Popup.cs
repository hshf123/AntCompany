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

        if (Managers.Data.StageDict.TryGetValue((int)StageLevel, out _stageData) == false)
        {
            Debug.Log($"Failed to load {StageLevel} data");
            return false;
        }

        Managers.Game.MonsterCount = _stageData.MonsterCount;

        CreateMonster();
        Managers.Sound.Play("Sound_Battle", Define.Sound.Bgm);

        return true;
    }

    void Update()
    {
        int hp = Managers.Game.HP;
        if (hp <= 0)
        {
            hp = 0;
            StageEnd();
        }
        GetText((int)Texts.HpText).text = hp.ToString();
        float ratio = hp / (float)Managers.Game.MaxHP;
        if (ratio <= 0.5f)
            GetText((int)Texts.HpText).color = Color.black;
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(ratio);
    }

    void StageEnd()
    {
        // TODO : 스테이지 종료

    }

    void OnClickSkillButton1()
    {
        Managers.Resource.Instantiate("Objects/Range", transform);
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
