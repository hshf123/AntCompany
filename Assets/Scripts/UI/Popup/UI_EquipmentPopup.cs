using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentPopup : UI_Popup
{
    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum Buttons
    {
        EquipmentButton1,
        EquipmentButton2,
        EquipmentButton3,
        EquipmentButton4,
        EquipmentListButton1,
        EquipmentListButton2,
        EquipmentListButton3,
        EquipmentListButton4,
        EquipmentListButton5,
        EquipmentListButton6,
        EquipmentListButton7,
        EquipmentListButton8,
        EquipmentListButton9,
        EquipmentListButton10,
        EquipmentListButton11,
        EquipmentListButton12,
        EquipmentListButton13,
        EquipmentListButton14,
        EquipmentListButton15,
        EquipmentListButton16,
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
        Managers.UI.ShowPopupUI<UI_SkillPopup>();
        Managers.UI.ClosePopupUI(this);
    }
    void OnClickStageAndBossButton()
    {
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
        Managers.UI.ClosePopupUI(this);
    }
    void OnClickEquipmentWindowButton()
    {
        Debug.Log("OnClickEquipmentWindowButton");
    }
}
