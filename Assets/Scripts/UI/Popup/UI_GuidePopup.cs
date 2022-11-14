using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GuidePopup : UI_Popup
{
    int _currentPage = (int)GameObjects.Intro1;
    int _lastPage = (int)GameObjects.Guide3 + 1;

    enum GameObjects
    {
        Intro1,
        Intro2,
        Intro3,
        Guide1,
        Guide2,
        Guide3,
    }

    enum Texts
    {
        GuideText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        gameObject.BindEvent(OnClickImage);

        for (int i = _currentPage + 1; i < _lastPage; i++)
        {
            Get<GameObject>(i).SetActive(false);
        }

        GetText((int)Texts.GuideText).text = "스테이지를 클리어하고 \n 레벨을 올리세요!";

        return true;
    }

    void OnClickImage()
    {
        Managers.Sound.Play("Sound_MainButton");
        Get<GameObject>(_currentPage).SetActive(false);
        _currentPage += 1;
        if (_currentPage == _lastPage)
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_PlayPopup>();
            return;
        }

        Get<GameObject>(_currentPage).SetActive(true);

        switch (_currentPage)
        {
            case (int)GameObjects.Intro2:
                GetText((int)Texts.GuideText).text = "레벨을 올리고 \n 더 강력한 스킬을 사용하세요!";
                break;
            case (int)GameObjects.Intro3:
                GetText((int)Texts.GuideText).text = "레벨을 올리고 \n 더 강력한 장비를 사용하세요!";
                break;
            case (int)GameObjects.Guide1:
                GetText((int)Texts.GuideText).text = "보스를 물리쳐보세요!";
                break;
            case (int)GameObjects.Guide2:
                GetText((int)Texts.GuideText).text = "승리하여 보상을 획득하세요!";
                break;
            case (int)GameObjects.Guide3:
                GetText((int)Texts.GuideText).text = "정보를 확인하며 플레이해보세요!";
                break;
            default:
                GetText((int)Texts.GuideText).gameObject.SetActive(false);
                break;
        }
    }
}
