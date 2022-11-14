using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryPopup : UI_Popup
{
    // 슬롯 번호 1~16, 버튼
    Dictionary<int, GameObject> _buttons = new Dictionary<int, GameObject>();
    // 슬롯 번호 1~4, 버튼
    Dictionary<int, GameObject> _wearingButtons = new Dictionary<int, GameObject>();

    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum Buttons
    {
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

        // 장착한 아이템 리스트
        GameObject equipmentSet = Utils.FindChild(gameObject, "EquipmentSet");
        for (int i = 0; i < 4; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/EquipmentButton", equipmentSet.transform).gameObject;
            GameObject icon = button.FindChild("EquipmentButtonIcon").gameObject;
            icon.SetActive(false);
            _wearingButtons.Add(i + 1, icon);
            button.BindEvent(() =>
            {
                Equipment equipment;
                if (Managers.Game.Wearing.TryGetValue(i + 1, out equipment))
                    Managers.Game.ClearEquipment(i);
            });
        }

        // 아이템 리스트
        GameObject list = Utils.FindChild(gameObject, "EquipmentList");
        for (int i = 0; i < 16; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/EquipmentListButton", list.transform).gameObject;
            button.FindChild("EquipmentButtonIcon").SetActive(false);
            _buttons.Add(i, button);
        }

        #region 아이템 리스트 버튼 이벤트 설정
        int equipmentCount = 0;
        foreach (Equipment equipment in Managers.Data.EquipmentDict.Values)
        {
            GameObject icon = _buttons[equipmentCount].FindChild("EquipmentButtonIcon");
            icon.SetActive(true);
            _buttons[equipmentCount].BindEvent(() =>
            {
                Managers.UI.ShowPopupUI<UI_EquipmentPopup>();
                Managers.Inven.SelectedItem = equipment;
                Debug.Log("Click");
            });

            Image image = icon.GetComponent<Image>();
            image.sprite = Managers.Resource.Load<Sprite>(equipment.Path);

            equipmentCount++;
        }
        #endregion

        RefreshUI();

        return true;
    }

    public void RefreshUI()
    {
        #region 텍스트 설정
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.Name;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
        #endregion
        #region 장착한 아이템 설정
        for (int i = 0; i < 4; i++)
        {
            Equipment equipment;
            if (Managers.Game.Wearing.TryGetValue(i, out equipment))
            {
                // 장착한 장비가 있을 경우
                GameObject go;
                if (_wearingButtons.TryGetValue(i + 1, out go) == false)
                {
                    Debug.Log("Failed to find equipment list");
                    return;
                }
                Image image = go.GetComponent<Image>();
                image.sprite = Managers.Resource.Load<Sprite>(equipment.Path);
                go.SetActive(true);
            }
        }
        #endregion
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
