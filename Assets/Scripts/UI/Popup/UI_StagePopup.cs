using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StagePopup : UI_Popup
{
    public StageLevels StageLevel { get; set; } = StageLevels.None;
    
    Stage _stageData;
    float _gameTime = 60f;
    float _elapsedTime = 0f;
    float _checkTime = 0f;
    Coroutine _coSkillCoolTime;
    bool _isCoolDown = false;

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
        ButtonCoolTime,
        SkillButtonIcon2,
        SkillButtonIcon3,
        SkillButtonIcon4
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

        StageLevel = StageLevels.Stage1;

        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Get<Button>((int)Buttons.SkillButton1).gameObject.BindEvent(OnClickSkillButton1);
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(Managers.Game.HP);
        GetText((int)Texts.HpText).text = Managers.Game.HP.ToString();
        GetText((int)Texts.RemainingTimeText).text = _gameTime.ToString();

        if (Managers.Data.StageDict.TryGetValue((int)StageLevel, out _stageData) == false)
        {
            Debug.Log($"Failed to load {StageLevel} data");
            return false;
        }

        Managers.Game.MonsterCount = _stageData.MonsterCount;

        CreateMonster();
        Managers.Sound.Play("Sound_Battle", Define.Sound.Bgm);

        return true;
    }

    void Update()
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
        CheckGameTime();
    }

    void CheckGameTime()
    {
        _checkTime += Time.deltaTime;
        if (_checkTime > 1f)
        {
            _gameTime -= 1f;
            _elapsedTime += 1f;
            _checkTime = 0f;
        }
        GetText((int)Texts.RemainingTimeText).text = _gameTime.ToString();
    }

    protected void CreateMonster()
    {
        for(int i=0; i<Managers.Game.MonsterCount; i++)
        {
            GameObject monster = Managers.Resource.Instantiate("Objects/Monster", gameObject.transform);
            MonsterController mc = monster.GetComponent<MonsterController>();
            Managers.Game.Monsters.Add(mc);
            mc.SetStage(gameObject);
        }
    }

    void StageEnd()
    {
        // TODO : 스테이지 종료

    }

    void OnClickSkillButton1()
    {
        if(_isCoolDown == false)
        {
            _isCoolDown = true;
            Managers.Resource.Instantiate("Objects/Range", transform);
            _coSkillCoolTime = StartCoroutine("CoSkillCoolTime", 8f);
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
        
        while(seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        _isCoolDown = false;
    }
}
