using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfoPopup : UI_Popup
{
    int _currentBox = (int)GameObjects.InfoTextBox;

    enum GameObjects
    {
        InfoTextBox,
        WearingTextBox,
    }

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
        SkillText1,
        SkillText2,
        SkillText3,
        SkillText4,
    }

    enum Buttons
    {
        NextButton,
        OKButton
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Bind<Button>((typeof(Buttons)));

        Get<Button>((int)Buttons.NextButton).gameObject.BindEvent(OnClickNextButton);
        Get<Button>((int)Buttons.OKButton).gameObject.BindEvent(OnClickOKButton);

        Get<GameObject>(_currentBox + 1).SetActive(false);

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
                GetText(i).text = "없음";
            else
                GetText(i).text = equipment.Name;
            slot++;
        }

        slot = 0;
        for (int i = (int)Texts.SkillText1; i <= (int)Texts.SkillText4; i++)
        {
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(slot, out skill) == false)
                GetText(i).text = "없음";
            else
                GetText(i).text = skill.Name;
            slot++;
        }
    }

    void OnClickNextButton()
    {
        Get<GameObject>(_currentBox).SetActive(false);
        Get<GameObject>(ChangeTextBox()).SetActive(true);
    }
    void OnClickOKButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
    }
    int ChangeTextBox()
    {
        if (_currentBox == (int)GameObjects.InfoTextBox)
            _currentBox = (int)GameObjects.WearingTextBox;
        else
            _currentBox = (int)GameObjects.InfoTextBox;

        return _currentBox;
    }
}
