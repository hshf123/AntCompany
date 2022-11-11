using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillPopup : UI_Popup
{
    // ���� ��ȣ 1~16, ��ư
    Dictionary<int, GameObject> _buttons = new Dictionary<int, GameObject>();
    // ���� ��ȣ 1~4, ��ư
    Dictionary<int, GameObject> _skillButtons = new Dictionary<int, GameObject>();

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

        GameObject list = Utils.FindChild(gameObject, "SkillList");
        for (int i = 0; i < 16; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/SkillListButton", list.transform).gameObject;
            button.FindChild("SkillButtonIcon").SetActive(false);
            _buttons.Add(i, button);
        }
        GameObject skillSet = Utils.FindChild(gameObject, "SkillSet");
        for (int i = 0; i < 4; i++)
        {
            GameObject button = Managers.Resource.Instantiate("UI/SubItem/SkillButton", skillSet.transform).gameObject;
            GameObject icon = button.FindChild("SkillButtonIcon").gameObject;
            icon.SetActive(false);
            _skillButtons.Add(i + 1, icon);
        }

        #region ������ ��ų ����Ʈ ����
        int skillCount = 0;
        foreach (Skill skill in Managers.Data.SkillDict.Values)
        {
            GameObject icon = _buttons[skillCount].FindChild("SkillButtonIcon");
            icon.SetActive(true);
            _buttons[skillCount].BindEvent(() =>
            {
                Managers.UI.ShowPopupUI<UI_SkillSelectPopup>();
                Managers.Skill.SelectedSkill = skill;
                Debug.Log("Click");
            });

            Image image = icon.GetComponent<Image>();
            image.sprite = Managers.Resource.Load<Sprite>(skill.Path);

            skillCount++;
        }
        #endregion

        RefreshUI();

        return true;
    }

    public void RefreshUI()
    {
        #region �ؽ�Ʈ ����
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.Name;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
        #endregion
        #region ������ ��ų ����
        for (int i = 0; i < 4; i++)
        {
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(i, out skill))
            {
                // ������ ��� ���� ���
                GameObject go;
                if (_skillButtons.TryGetValue(i + 1, out go) == false)
                {
                    Debug.Log("Failed to find skill list");
                    return;
                }
                // TODO : ������ ��δ� �����Ϳ� ����
                Image image = go.GetComponent<Image>();
                image.sprite = Managers.Resource.Load<Sprite>(skill.Path);
                go.SetActive(true);
            }
        }
        #endregion
    }

    void OnClickSkillWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Debug.Log("OnClickSkillWindowButton");
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
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_InventoryPopup>();
    }
}
