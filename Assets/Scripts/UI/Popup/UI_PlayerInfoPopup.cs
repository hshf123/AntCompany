using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfoPopup : UI_Popup
{
    enum Texts
    {
        NameText,
        LevelText,
        MoneyText,
        ExpText,
        AttackText,
        AttackSpeedText,
        MaxHpText,
        EquipmentText1,
        EquipmentText2,
        EquipmentText3,
        EquipmentText4,
    }

    enum Buttons
    {
        OKButton
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        Bind<Button>((typeof(Buttons)));

        Get<Button>((int)Buttons.OKButton).gameObject.BindEvent(OnClickOKButton);

        SetTexts();

        return true;
    }

    void SetTexts()
    {
        GetText((int)Texts.NameText).text = Managers.Game.Name;
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
        GetText((int)Texts.ExpText).text = Managers.Game.Exp.ToString();
        GetText((int)Texts.AttackText).text = Managers.Game.TotalAttack.ToString();
        GetText((int)Texts.AttackSpeedText).text = Managers.Game.TotalAttackSpeed.ToString();
        GetText((int)Texts.MaxHpText).text = Managers.Game.TotalMaxHP.ToString();

        int slot = 0;
        for (int i = (int)Texts.EquipmentText1; i <= (int)Texts.EquipmentText4; i++)
        {
            Equipment equipment;
            if (Managers.Game.Wearing.TryGetValue(slot, out equipment) == false)
                GetText(i).text = "¾øÀ½";
            else
                GetText(i).text = equipment.Name;
            slot++;
        }
    }

    void OnClickOKButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
    }
}
