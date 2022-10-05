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

        for(int i=_currentPage+1; i<_lastPage; i++)
        {
            Get<GameObject>(i).SetActive(false);
        }

        return true;
    }

    void OnClickImage()
    {
        Debug.Log("Click");
        Managers.Sound.Play("Sound_MainButton");
        Get<GameObject>(_currentPage).SetActive(false);
        _currentPage += 1;
        if (_currentPage == _lastPage)
            return;
        Get<GameObject>(_currentPage).SetActive(true);

        switch(_currentPage)
        {
            case (int)GameObjects.Intro2:
                GetText((int)Texts.GuideText).text = "ù ȸ��� ����! �ʹ� �ູ�� �� ���� \n ������ ȸ�縦 �ٳ༭ ���ڰ� �� �ž�!";
                break;
            case (int)GameObjects.Intro3:
                GetText((int)Texts.GuideText).text = "������ ����� �������� �ɱ�?";
                break;
            default:
                GetText((int)Texts.GuideText).gameObject.SetActive(false);
                break;
        }
    }
}
