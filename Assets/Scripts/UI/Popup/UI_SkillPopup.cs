using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillPopup : UI_Popup
{
    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum Buttons
    {
        SkillButton1,
        SkillButton2,
        SkillButton3,
        SkillButton4,
        SkillListButton1,
        SkillListButton2,
        SkillListButton3,
        SkillListButton4,
        SkillListButton5,
        SkillListButton6,
        SkillListButton7,
        SkillListButton8,
        SkillListButton9,
        SkillListButton10,
        SkillListButton11,
        SkillListButton12,
        SkillListButton13,
        SkillListButton14,
        SkillListButton15,
        SkillListButton16,
        SkillEnhancementButton,
        StageAndBossButton,
        EquipmentFortificationButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        BindText(typeof(Texts));

        Get<Button>((int)Buttons.SkillEnhancementButton).gameObject.BindEvent(OnClickSkillEnhancementButton);
        Get<Button>((int)Buttons.StageAndBossButton).gameObject.BindEvent(OnClickStageAndBossButton);
        Get<Button>((int)Buttons.EquipmentFortificationButton).gameObject.BindEvent(OnClickEquipmentFortificationButton);

        RefreshUI();

        return true;
    }

    void RefreshUI()
    {
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.NickName;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
    }


    void OnClickSkillEnhancementButton()
    {
        Debug.Log("OnClickSkillEnhancementButton");
    }
    void OnClickStageAndBossButton()
    {
        Debug.Log("OnClickStageAndBossButton");
    }
    void OnClickEquipmentFortificationButton()
    {
        Debug.Log("OnClickEquipmentFortificationButton");
    }
}
