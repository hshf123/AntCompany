using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryPopup : UI_Popup
{
    List<GameObject> _buttons = new List<GameObject>();

    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum Buttons
    {
        EquipmentButton1,
        EquipmentButton2,
        EquipmentButton3,
        EquipmentButton4,
        SkillWindowButton,
        StageAndBossButton,
        InventoryWindowButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        BindText(typeof(Texts));

        Get<Button>((int)Buttons.SkillWindowButton).gameObject.BindEvent(OnClickSkillWindowButton);
        Get<Button>((int)Buttons.StageAndBossButton).gameObject.BindEvent(OnClickStageAndBossButton);
        Get<Button>((int)Buttons.InventoryWindowButton).gameObject.BindEvent(OnClickInventoryWindowButton);

        RefreshUI();

        return true;
    }

    void RefreshUI()
    {
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.Name;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();

        GameObject go = Utils.FindChild(gameObject, "EquipmentList");
        for (int i = 0; i < 16; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/EquipmentListButton", go.transform).gameObject;
            button.FindChild("EquipmentButtonIcon").SetActive(false);
            _buttons.Add(button);
        }

        int equipmentCount = 0;
        foreach (BowData bow in Managers.Inven.Items.Values)
        {
            GameObject icon = _buttons[equipmentCount].FindChild("EquipmentButtonIcon");
            icon.SetActive(true);
            // TODO : 장비 선택
            _buttons[equipmentCount].BindEvent(() => { Managers.UI.ShowPopupUI<UI_EquipmentPopup>(); Debug.Log("Click"); });
            Image image = icon.GetComponent<Image>();

            // TODO : 아이콘 경로는 데이터에 저장
            image.sprite = Managers.Resource.Load<Sprite>("Sprites/Popup3/fire_01");

            equipmentCount++;
        }
    }

    void OnClickSkillWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_SkillPopup>();
    }
    void OnClickStageAndBossButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
    void OnClickInventoryWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Debug.Log("OnClickInventoryWindowButton");
    }
}
