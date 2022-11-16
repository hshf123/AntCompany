using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BaseStagePopup : UI_Popup
{
    protected bool _isCoolDown1 = false;
    protected bool _isCoolDown2 = false;
    protected bool _isCoolDown3 = false;
    protected bool _isCoolDown4 = false;
    protected bool _isStageEnd = false;
    protected bool _stop = false;

    enum Images
    {
        SkillButtonIcon1,
        SkillButtonIcon2,
        SkillButtonIcon3,
        SkillButtonIcon4,
        ButtonCoolTime1,
        ButtonCoolTime2,
        ButtonCoolTime3,
        ButtonCoolTime4,
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

        UI_MainScene mainUI = transform.parent.gameObject.FindChild("UI_MainScene").GetComponent<UI_MainScene>();
        if(mainUI!=null)
            mainUI.ButtonActivate();

        Time.timeScale = 1;

        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        Get<Button>((int)Buttons.SkillButton1).gameObject.BindEvent(OnClickSkillButton1);
        Get<Button>((int)Buttons.SkillButton2).gameObject.BindEvent(OnClickSkillButton2);
        Get<Button>((int)Buttons.SkillButton3).gameObject.BindEvent(OnClickSkillButton3);
        Get<Button>((int)Buttons.SkillButton4).gameObject.BindEvent(OnClickSkillButton4);
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(Managers.Game.TotalMaxHP);
        GetText((int)Texts.HpText).text = Managers.Game.TotalMaxHP.ToString();

        for (int i = (int)Images.SkillButtonIcon1; i <= (int)Images.SkillButtonIcon4; i++)
        {
            Image image = Get<Image>(i);
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(i, out skill))
                image.sprite = Managers.Resource.Load<Sprite>(skill.Path);
            else
                image.gameObject.SetActive(false);
        }

        Managers.Sound.Play("Sound_Battle", Define.Sound.Bgm);
        StartCoroutine(CheckGameTime());

        return true;
    }

    protected void UpdateHp()
    {
        int hp = Managers.Game.TotalHP;
        if (hp <= 0)
        {
            hp = 0;
            StageEnd();
        }
        GetText((int)Texts.HpText).text = hp.ToString();
        float ratio = hp / (float)Managers.Game.TotalMaxHP;
        if (ratio <= 0.5f)
            GetText((int)Texts.HpText).color = Color.black;
        Get<GameObject>((int)GameObjects.Wall).GetComponent<HpBar>().SetHpBar(ratio);
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

    protected void StageEnd()
    {
        if (_isStageEnd == true)
            return;
        Debug.Log($"Stage End");
        Managers.UI.ShowPopupUI<UI_StageEndPopup>();
        _isStageEnd = true;
        Time.timeScale = 0;
    }

    protected void OnClickSkillButton1()
    {
        if (_isCoolDown1 == false)
        {
            _isCoolDown1 = true;
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(0, out skill))
            {
                CheckSkillType(skill);
                StartCoroutine(CoSkillCoolTime1(skill.CoolTime));
            }
            else
            {
                Debug.Log("Failed to load skill data");
                return;
            }
        }
    }
    protected void OnClickSkillButton2()
    {
        if (_isCoolDown2 == false)
        {
            _isCoolDown2 = true;
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(1, out skill))
            {
                CheckSkillType(skill);
                StartCoroutine(CoSkillCoolTime2(skill.CoolTime));
            }
            else
            {
                Debug.Log("Failed to load skill data");
                return;
            }
        }
    }
    protected void OnClickSkillButton3()
    {
        if (_isCoolDown3 == false)
        {
            _isCoolDown3 = true;
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(2, out skill))
            {
                CheckSkillType(skill);
                StartCoroutine(CoSkillCoolTime3(skill.CoolTime));
            }
            else
            {
                Debug.Log("Failed to load skill data");
                return;
            }
        }
    }
    protected void OnClickSkillButton4()
    {
        if (_isCoolDown4 == false)
        {
            _isCoolDown4 = true;
            Skill skill;
            if (Managers.Game.Skills.TryGetValue(3, out skill))
            {
                CheckSkillType(skill);
                StartCoroutine(CoSkillCoolTime4(skill.CoolTime));
            }
            else
            {
                Debug.Log("Failed to load skill data");
                return;
            }
        }
    }

    protected IEnumerator CoSkillCoolTime1(float seconds)
    {
        float coolTime = seconds;

        while (seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime1).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Get<Image>((int)Images.ButtonCoolTime1).fillAmount = 0;
        _isCoolDown1 = false;
    }
    protected IEnumerator CoSkillCoolTime2(float seconds)
    {
        float coolTime = seconds;

        while (seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime2).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Get<Image>((int)Images.ButtonCoolTime1).fillAmount = 0;
        _isCoolDown2 = false;
    }
    protected IEnumerator CoSkillCoolTime3(float seconds)
    {
        float coolTime = seconds;

        while (seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime3).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Get<Image>((int)Images.ButtonCoolTime1).fillAmount = 0;
        _isCoolDown3 = false;
    }
    protected IEnumerator CoSkillCoolTime4(float seconds)
    {
        float coolTime = seconds;

        while (seconds >= 0f)
        {
            Get<Image>((int)Images.ButtonCoolTime4).fillAmount = (seconds / coolTime);
            seconds -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Get<Image>((int)Images.ButtonCoolTime1).fillAmount = 0;
        _isCoolDown4 = false;
    }

    protected void CheckSkillType(Skill skill)
    {
        switch ((Define.SkillType)skill.Type)
        {
            case Define.SkillType.Range:
                {
                    int damage = (skill as RangeSkill).Damage;
                    OnRangeSkill(damage);
                }
                break;
            case Define.SkillType.Target:
                {
                    int damage = (skill as TargetSkill).Damage;
                    OnTargetSkill(damage);
                }
                break;
            case Define.SkillType.Debuff:
                {
                    float duration = (skill as DebuffSkill).Duration;
                    int speed = (skill as DebuffSkill).Speed;
                    OnDebuffSkill(duration, speed);
                }
                break;
            case Define.SkillType.Buff:
                {
                    float duration = (skill as BuffSkill).Duration;
                    float attackSpeed = (skill as BuffSkill).AttackSpeed;
                    OnBuffSkill(duration, attackSpeed);
                }
                break;
        }
    }
    protected void OnRangeSkill(int damage)
    {
        Debug.Log($"Range Skill {damage}");
        Managers.Resource.Instantiate("Objects/Range", transform).GetComponent<RangeController>().SetDamage(damage);
    }
    protected void OnTargetSkill(int damage)
    {
        Debug.Log($"Target Skill {damage}");
        CreatureController cc = Managers.Game.Player.Target;
        if (cc != null)
            cc.OnDamaged(damage);
    }
    protected void OnDebuffSkill(float duration, int speed)
    {
        Debug.Log($"Debuff Skill Duration : {duration} / Speed : {speed}");
        StartCoroutine(CoDebuffSkill(duration, speed));
    }
    protected void OnBuffSkill(float duration, float attackSpeed)
    {
        Debug.Log($"Buff Skill Duration : {duration} / Attack Speed : {attackSpeed}");
        StartCoroutine(CoBuffSkill(duration, attackSpeed));
    }

    protected IEnumerator CoDebuffSkill(float duration, int speed)
    {
        foreach (CreatureController cc in Managers.Game.Creatures)
            cc.Speed -= speed;

        while (duration >= 0f)
        {
            duration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        foreach (CreatureController cc in Managers.Game.Creatures)
            cc.Speed += speed;
    }
    protected IEnumerator CoBuffSkill(float duration, float attackSpeed)
    {
        Managers.Game.TotalAttackSpeed += attackSpeed;

        while (duration >= 0f)
        {
            duration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Managers.Game.TotalAttackSpeed -= attackSpeed;
    }
}
