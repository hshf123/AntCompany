using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.ShowPopupUI<UI_TitlePopup>();
        Managers.UI.ShowSceneUI<UI_SoundIconScene>();

        return true;
    }
}
