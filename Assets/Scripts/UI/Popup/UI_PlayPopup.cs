using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayPopup : UI_Popup
{
    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum Buttons
    { 
        StageButton,
        BossButton,
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

        Get<Button>((int)Buttons.StageButton).gameObject.BindEvent(OnClickStageButton);
        Get<Button>((int)Buttons.BossButton).gameObject.BindEvent(OnClickBossButton);
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

    void OnClickStageButton()
    {
        Debug.Log("OnClickStageButton");
    }
    void OnClickBossButton()
    {
        Debug.Log("OnClickBossButton");
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
