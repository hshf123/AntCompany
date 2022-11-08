using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StagePopup : UI_Popup
{
    public StageLevels StageLevel { get; set; } = StageLevels.None;

    Stage _stageData;
    bool _isCoolDown = false;
    bool _isEnd = false;

    public enum StageLevels
    {
        None,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }

    enum Images
    {
        SkillButtonIcon1,
        ButtonCoolTime1,
        SkillButtonIcon2,
        ButtonCoolTime2,
        SkillButtonIcon3,
        ButtonCoolTime3,
        SkillButtonIcon4,
        ButtonCoolTime4
    }

    enum Buttons
    {
        SkillButton1,
        SkillButton2,
        SkillButton3,
        SkillButton4,
    }

    enum GameObjects
    {
        Wall,
    }

    enum Texts
    {
        HpText,
        RemainingTimeText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Get<Button>((int)Buttons.SkillButton1).gameObject.BindEvent(OnClickSkillButton1);
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(Managers.Game.HP);
        GetText((int)Texts.HpText).text = Managers.Game.HP.ToString();

        if (Managers.Data.StageDict.TryGetValue((int)StageLevel, out _stageData) == false)
        {
            Debug.Log($"Failed to load {StageLevel} data");
            return false;
        }
        Managers.Game.MonsterCount = _stageData.MonsterCount;
        // TODO : 스킬 정보 세팅

        CreateMonster();
        Managers.Sound.Play("Sound_Battle", Define.Sound.Bgm);
        StartCoroutine(CheckGameTime());

        return true;
    }

    void Update()
    {
        UpdateHp();
        UpdateMonsterCount();
    }

    void UpdateHp()
    {
        int hp = Managers.Game.HP;
        if (hp <= 0)
        {
            hp = 0;
            StageEnd();
        }
        GetText((int)Texts.HpText).text = hp.ToString();
        float ratio = hp / (float)Managers.Game.MaxHP;
        if (ratio <= 0.5f)
            GetText((int)Texts.HpText).color = Color.black;
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(ratio);
    }
    void UpdateMonsterCount()
    {
        if (Managers.Game.Monsters.Count == 0)
            StageEnd();
    }

    IEnumerator CheckGameTime()
    {
        int playTime = 60;
        while (playTime >= 0)
        {
            GetText((int)Texts.RemainingTimeText).text = playTime.ToString();
            yield return new WaitForSeconds(1);
            playTime -= 1;
        }
        if (playTime == 0)
            Managers.Game.OnDamaged(999999);
    }

    protected void CreateMonster()
    {
        for (int i = 0; i < Managers.Game.MonsterCount; i++)
        {
            GameObject monster = Managers.Resource.Instantiate("Objects/Monster", gameObject.transform);
            MonsterController mc = monster.GetComponent<MonsterController>();
            Managers.Game.Monsters.Add(mc);
            mc.SetStage(gameObject);
        }
    }

    void StageEnd()
    {
        if (_isEnd == true)
            return;
        Debug.Log($"Stage End");
        Managers.UI.ShowPopupUI<UI_StageEndPopup>();
        _isEnd = true;
        Time.timeScale = 0;
    }

    void OnClickSkillButton1()
    {
        if (_isCoolDown == false)
        {
            _isCoolDown = true;
            Managers.Resource.Instantiate("Objects/Range", transform);
            StartCoroutine(CoSkillCoolTime(8f));
        }
    }
    void OnClickSkillButton2()
    {

    }
    void OnClickSkillButton3()
    {

    }
    void OnClickSkillButton4()
    {

    }

    IEnumerator CoSkillCoolTime(float seconds)
    {
        float coolTime = seconds;

        while (seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime1).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        _isCoolDown = false;
    }
}
