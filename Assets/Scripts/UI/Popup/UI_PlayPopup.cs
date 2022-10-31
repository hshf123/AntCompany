using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayPopup : UI_Popup
{
    enum Texts
    {
        LevelText,
        NickNameText,
        MoneyText,
    }

    enum GameObjects
    {
        StageList,
    }

    enum Buttons
    {
        StageButton,
        BossButton,
        SkillWindowButton,
        StageAndBossButton,
        EquipmentWindowButton,
        StageButton1,
        StageButton2,
        StageButton3,
        StageButton4,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        BindText(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.StageButton).gameObject.BindEvent(OnClickStageButton);
        Get<Button>((int)Buttons.BossButton).gameObject.BindEvent(OnClickBossButton);
        Get<Button>((int)Buttons.SkillWindowButton).gameObject.BindEvent(OnClickSkillWindowButton);
        Get<Button>((int)Buttons.StageAndBossButton).gameObject.BindEvent(OnClickStageAndBossButton);
        Get<Button>((int)Buttons.EquipmentWindowButton).gameObject.BindEvent(OnClickEquipmentWindowButton);
        Get<Button>((int)Buttons.StageButton1).gameObject.BindEvent(OnClickStage1InitButton);
        Get<Button>((int)Buttons.StageButton2).gameObject.BindEvent(OnClickStage2InitButton);
        Get<Button>((int)Buttons.StageButton3).gameObject.BindEvent(OnClickStage3InitButton);
        Get<Button>((int)Buttons.StageButton4).gameObject.BindEvent(OnClickStage4InitButton);

        Get<GameObject>((int)GameObjects.StageList).gameObject.SetActive(false);

        RefreshUI();

        return true;
    }

    void RefreshUI()
    {
        GetText((int)Texts.LevelText).text = Managers.Game.Level.ToString();
        GetText((int)Texts.NickNameText).text = Managers.Game.Name;
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
    }

    void OnClickStageButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Get<Button>((int)Buttons.StageButton).gameObject.SetActive(false);
        Get<Button>((int)Buttons.BossButton).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.StageList).gameObject.SetActive(true);
    }
    void OnClickBossButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Debug.Log("OnClickBossButton");
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
        Debug.Log("OnClickStageAndBossButton");
    }
    void OnClickEquipmentWindowButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_EquipmentPopup>();
    }

#region 스테이지
    void OnClickStage1InitButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StagePopup>().StageLevel = UI_StagePopup.StageLevels.Stage1;
    }
    void OnClickStage2InitButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StagePopup>().StageLevel = UI_StagePopup.StageLevels.Stage2;
    }
    void OnClickStage3InitButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StagePopup>().StageLevel = UI_StagePopup.StageLevels.Stage3;
    }
    void OnClickStage4InitButton()
    {
        Managers.Sound.Play("Sound_MainButton", Define.Sound.Effect);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StagePopup>().StageLevel = UI_StagePopup.StageLevels.Stage4;
    }
#endregion
}
