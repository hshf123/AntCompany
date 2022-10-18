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
        SkillWindowButton,
        StageAndBossButton,
        EquipmentWindowButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        BindText(typeof(Texts));

        Get<Button>((int)Buttons.StageButton).gameObject.BindEvent(OnClickStageButton);
        Get<Button>((int)Buttons.BossButton).gameObject.BindEvent(OnClickBossButton);
        Get<Button>((int)Buttons.SkillWindowButton).gameObject.BindEvent(OnClickSkillWindowButton);
        Get<Button>((int)Buttons.StageAndBossButton).gameObject.BindEvent(OnClickStageAndBossButton);
        Get<Button>((int)Buttons.EquipmentWindowButton).gameObject.BindEvent(OnClickEquipmentWindowButton);

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

    void OnClickSkillWindowButton()
    {
        Managers.UI.ShowPopupUI<UI_SkillPopup>();
        Managers.UI.ClosePopupUI(this);
    }
    void OnClickStageAndBossButton()
    {
        Debug.Log("OnClickStageAndBossButton");
    }
    void OnClickEquipmentWindowButton()
    {
        Managers.UI.ShowPopupUI<UI_EquipmentPopup>();
        Managers.UI.ClosePopupUI(this);
    }
}
