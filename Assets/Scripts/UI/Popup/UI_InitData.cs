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
        // TODO : 모든 데이터 날리고 새로 시작
        

        Debug.Log("데이터 초기화");
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_InputNickNamePopup>();
        Managers.Sound.Clear();
    }
    void OnCancelButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
    }
}
