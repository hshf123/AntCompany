using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelectPopup : UI_Popup
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

        Get<Button>((int)Buttons.CheckButton).gameObject.BindEvent(OnClickCheckButton);
        Get<Button>((int)Buttons.CancelButton).gameObject.BindEvent(OnClickCancelButton);

        return true;
    }

    void OnClickCheckButton()
    {
        Managers.Game.SelectSkill(Managers.Game.SkillSlotNumber++ % 4, Managers.Skill.SelectedSkill);
        Managers.UI.ClosePopupUI();
        Managers.UI.Root.FindChild("UI_SkillPopup").GetComponent<UI_SkillPopup>().RefreshUI();
        Managers.Game.Save();
    }
    void OnClickCancelButton()
    {
        Managers.UI.ClosePopupUI();
    }
}
