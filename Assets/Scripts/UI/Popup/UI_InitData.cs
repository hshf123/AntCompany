using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InitData : UI_Popup
{
    enum Buttons
    {
        CheckButton,
        CancelButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.CheckButton).gameObject.BindEvent(OnCheckButton);
        Get<Button>((int)Buttons.CancelButton).gameObject.BindEvent(OnCancelButton);

        return true;
    }

    void OnCheckButton()
    {
        Debug.Log("데이터 초기화");
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);

    }
    void OnCancelButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }
}
