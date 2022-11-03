using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossStagePopup : UI_Popup
{
    Boss _bossData;
    BossController _boss;
    bool _isCoolDown = false;
    bool _isEnd = false;

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

        // TODO 보스레벨
        //if (Managers.Data.....TryGetValue(1, out _bossData) == false)
        //{
        //    Debug.Log($"Failed to load {_bossData} data");
        //    return false;
        //}
        // TODO : 스킬 정보 세팅

        CreateBoss();
        Managers.Sound.Play("Sound_Battle", Define.Sound.Bgm);
        StartCoroutine(CheckGameTime());

        return true;
    }

    void Update()
    {
        UpdateHp();
        UpdateBossStage();
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
    void UpdateBossStage()
    {
        if (_boss.Hp == 0)
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

    protected void CreateBoss()
    {
        GameObject boss = Managers.Resource.Instantiate("Objects/Boss", gameObject.transform);
        _boss = boss.GetComponent<BossController>();
        //Managers.Game.Monsters.Add(bc);
        _boss.SetStage(gameObject);
    }

    void StageEnd()
    {
        if (_isEnd == true)
            return;
        Debug.Log($"Stage End");
        Managers.UI.ShowPopupUI<UI_StageEndPopup>();
        _isEnd = true;
    }

    void OnClickSkillButton1()
    {
        if (_isCoolDown == false)
        {
            _isCoolDown = true;
            Managers.Resource.Instantiate("Objects/Range", transform);
            StartCoroutine(CoSkillCoolTime(8f)); // TODO : 쿨타임 정보 데이터로
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
