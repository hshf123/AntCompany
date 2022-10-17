using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Top : UI_Base
{
    TextMeshProUGUI _levelText;
    TextMeshProUGUI _nickNameText;
    TextMeshProUGUI _moneyText;

    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        _levelText = GetText((int)Texts.LevelText);
        _nickNameText = GetText((int)Texts.NickNameText);
        _moneyText = GetText((int)Texts.MoneyText);

        return true;
    }
}
