using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentPopup : UI_Popup
{
    int _slotNum = 0;

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

        Get<Button>((int)Buttons.CheckButton).gameObject.BindEvent(OnClickCheckButton);
        Get<Button>((int)Buttons.CancelButton).gameObject.BindEvent(OnClickCancelButton);

        return true;
    }
    
    void OnClickCheckButton()
    {
        // TODO ¿Â∫Ò ¿Â¬¯
        Managers.Game.SelectEquipment(Managers.Game.SlotNumber++ % 4, Managers.Inven.SelectedItem);
        Managers.UI.ClosePopupUI();
    }
    void OnClickCancelButton()
    {
        Managers.UI.ClosePopupUI();
    }
}
