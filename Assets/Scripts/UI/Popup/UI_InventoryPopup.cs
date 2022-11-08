using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryPopup : UI_Popup
{
    // ���� ��ȣ 1~16, ��ư
    Dictionary<int, GameObject> _buttons = new Dictionary<int, GameObject>();
    // ���� ��ȣ 1~16, ��ư
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

        GameObject list = Utils.FindChild(gameObject, "EquipmentList");
        for (int i = 0; i < 16; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/EquipmentListButton", list.transform).gameObject;
            button.FindChild("EquipmentButtonIcon").SetActive(false);
            _buttons.Add(i, button);
        }
        GameObject equipmentSet = Utils.FindChild(gameObject, "EquipmentSet");
        for (int i = 0; i < 4; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/EquipmentButton", equipmentSet.transform).gameObject;
            GameObject icon = button.FindChild("EquipmentButtonIcon").gameObject;
            icon.SetActive(false);
            _wearingButtons.Add(i + 1, icon);
        }

        RefreshUI();

        return true;
    }

    void RefreshUI()
    {
        #region �ؽ�Ʈ ����
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.Name;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
        #endregion
        #region ������ ������ ����Ʈ ����
        int equipmentCount = 0;
        foreach (Equipment equipment in Managers.Data.EquipmentDict.Values)
        {
            GameObject icon = _buttons[equipmentCount].FindChild("EquipmentButtonIcon");
            icon.SetActive(true);
            // TODO : ��� ����
            _buttons[equipmentCount].BindEvent(() =>
            {
                Managers.UI.ShowPopupUI<UI_EquipmentPopup>();
                Managers.Inven.SelectedItem = equipment;
                Debug.Log("Click");
            });

            // TODO : ������ ��δ� �����Ϳ� ����
            Image image = icon.GetComponent<Image>();
            image.sprite = Managers.Resource.Load<Sprite>("Sprites/Popup3/fire_01");

            equipmentCount++;
        }
        #endregion
        #region ������ ������ ����
        for (int i = 0; i < 4; i++)
        {
            Equipment equipment;
            if (Managers.Game.Wearing.TryGetValue(i, out equipment))
            {
                // ������ ��� ���� ���
                GameObject go;
                if (_wearingButtons.TryGetValue(i + 1, out go) == false)
                {
                    Debug.Log("Failed to find equipment list");
                    return;
                }
                // TODO : ������ ��δ� �����Ϳ� ����
                Image image = go.GetComponent<Image>();
                image.sprite = Managers.Resource.Load<Sprite>("Sprites/Popup3/fire_01");
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
