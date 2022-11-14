using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainScene : UI_Scene
{
    bool _soundOnOff = true;
    Sprite _soundOnImage;
    Sprite _soundOffImage;
    bool _stop = false;

    enum Buttons
    {
        SoundOnOffButton,
        PlayerInfoButton,
        PauseButton,
    }

    enum Images
    {
        SoundOnOffButtonIcon,
        PauseButtonIcon,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.SoundOnOffButton).gameObject.BindEvent(OnClickSoundOnOffButton);
        Get<Button>((int)Buttons.PlayerInfoButton).gameObject.BindEvent(OnClickPlayerInfoButton);
        Get<Button>((int)Buttons.PauseButton).gameObject.BindEvent(OnClickStopButton);

        _soundOnImage = Managers.Resource.Load<Sprite>("Sprites/Icon/icon_sound_on");
        _soundOffImage = Managers.Resource.Load<Sprite>("Sprites/Icon/icon_sound_off");

        ButtonDeactivate();

        return true;
    }

    void OnClickSoundOnOffButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        if (_soundOnOff)
        {
            Managers.Sound.SoundOff();
            _soundOnOff = false;
            Get<Image>((int)Images.SoundOnOffButtonIcon).sprite = _soundOffImage;
        }
        else
        {
            Managers.Sound.SoundOn();
            _soundOnOff = true;
            Get<Image>((int)Images.SoundOnOffButtonIcon).sprite = _soundOnImage;
        }
    }
    void OnClickPlayerInfoButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_PlayerInfoPopup>();
    }
    void OnClickStopButton()
    {
        if (_stop)
        {
            Time.timeScale = 1;
            _stop = false;
            Get<Image>((int)Images.PauseButtonIcon).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/icon_stop");
        }
        else
        {
            Time.timeScale = 0;
            _stop = true;
            Get<Image>((int)Images.PauseButtonIcon).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/btn_30");
        }
    }

    public void ButtonActivate()
    {
        Get<Button>((int)Buttons.PauseButton).gameObject.SetActive(true);
    }
    public void ButtonDeactivate()
    {
        Get<Button>((int)Buttons.PauseButton).gameObject.SetActive(false);
    }
}
