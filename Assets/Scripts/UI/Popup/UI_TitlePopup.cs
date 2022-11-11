using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    enum Buttons
    {
        StartButton,
        ContinueButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        Get<Button>((int)Buttons.ContinueButton).gameObject.BindEvent(OnContinueButton);

        Managers.Sound.Play("Sound_MainTitle", Define.Sound.Effect);

        return true;
    }

    void OnStartButton()
    {
        Debug.Log("Start");
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_InitData>();
    }
    void OnContinueButton()
    {
        Debug.Log("Continue");
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);

        // TODO : 저장된 데이터 불러오기, 없다면 새로시작
        if (Managers.Game.LoadData() == false)
        {
            Managers.UI.ShowPopupUI<UI_InputNickNamePopup>();
        }
        else
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_PlayPopup>();
            Managers.Sound.Clear();
        }
    }
}
