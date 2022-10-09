using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup : UI_Popup
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
