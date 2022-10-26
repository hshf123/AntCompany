using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputNickNamePopup : UI_Popup
{
    enum Buttons
    {
        CheckButton,
        CancelButton,
    }

    enum Texts
    {
        ValueText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        BindText(typeof(Texts));

        Get<Button>((int)Buttons.CheckButton).gameObject.BindEvent(OnCheckButton);
        Get<Button>((int)Buttons.CancelButton).gameObject.BindEvent(OnCancelButton);

        return true;
    }

    void OnCheckButton()
    {
        Managers.Game.Name = GetText((int)Texts.ValueText).text;
        Debug.Log($"닉네임 저장 : {Managers.Game.Name}");
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_GuidePopup>();
        Managers.Sound.Clear();
    }
    void OnCancelButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
    }
}
