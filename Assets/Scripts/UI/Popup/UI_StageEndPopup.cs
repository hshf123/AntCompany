using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageEndPopup : UI_Popup
{
    enum Results
    {
        Victory,
        Defeat,
    }

    Results _results;

    enum GameObjects
    {
        VictoryImage,
        DefeatImage,
    }

    enum Texts
    {
        LevelBeforeText,
        ExpBeforeText,
        MoneyBeforeText,
        MaxHpBeforeText,
        AttackBeforeText,
        AttackSpeedBeforeText,
        LevelAfterText,
        ExpAfterText,
        MoneyAfterText,
        MaxHpAfterText,
        AttackAfterText,
        AttackSpeedAfterText,
    }

    enum Buttons
    {
        CheckButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Pool.Clear();

        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.CheckButton).gameObject.BindEvent(OnClickCheckButton);

        if (Managers.Game.TotalHP == 0)
            _results = Results.Defeat;
        else
            _results = Results.Victory;

        CheckGameResult();
        SetResultText();

        Managers.Game.Creatures.Clear();
        Managers.Game.LoadData();

        return true;
    }

    void CheckGameResult()
    {
        switch (_results)
        {
            case Results.Victory:
                Get<GameObject>((int)GameObjects.DefeatImage).SetActive(false);
                break;
            case Results.Defeat:
                Get<GameObject>((int)GameObjects.VictoryImage).SetActive(false);
                break;
        }
    }
    void Victory()
    {
        Debug.Log("Victory");
        Managers.Game.AddExp(150);
        Managers.Game.Money += 5000;
    }
    void Defeat()
    {
        Debug.Log("Defeat");
    }
    void SetResultText()
    {
        #region Before
        GetText((int)Texts.LevelBeforeText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.ExpBeforeText).text = Managers.Game.Exp.ToString();
        GetText((int)Texts.MoneyBeforeText).text = Managers.Game.Money.ToString();
        GetText((int)Texts.MaxHpBeforeText).text = Managers.Game.TotalMaxHP.ToString();
        GetText((int)Texts.AttackBeforeText).text = Managers.Game.TotalAttack.ToString();
        GetText((int)Texts.AttackSpeedBeforeText).text = Managers.Game.TotalAttackSpeed.ToString();
        #endregion
        switch (_results)
        {
            case Results.Victory:
                Victory();
                break;
            case Results.Defeat:
                Defeat();
                break;
        }
        #region After
        GetText((int)Texts.LevelAfterText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.ExpAfterText).text = Managers.Game.Exp.ToString();
        GetText((int)Texts.MoneyAfterText).text = Managers.Game.Money.ToString();
        GetText((int)Texts.MaxHpAfterText).text = Managers.Game.TotalMaxHP.ToString();
        GetText((int)Texts.AttackAfterText).text = Managers.Game.TotalAttack.ToString();
        GetText((int)Texts.AttackSpeedAfterText).text = Managers.Game.TotalAttackSpeed.ToString();
        #endregion
        Managers.Game.Save();
    }

    void OnClickCheckButton()
    {
        Managers.Sound.Clear();
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
