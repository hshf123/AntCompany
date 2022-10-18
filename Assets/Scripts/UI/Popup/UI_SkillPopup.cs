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


    void OnClickSkillWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Debug.Log("OnClickSkillWindowButton");
    }
    void OnClickStageAndBossButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
    void OnClickEquipmentWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_EquipmentPopup>();
    }
}
