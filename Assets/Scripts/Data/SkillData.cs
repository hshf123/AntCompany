using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData : ILoader<int, Skill>
{
    public List<RangeSkill> ranges = new List<RangeSkill>();
    public List<TargetSkill> targets = new List<TargetSkill>();
    public List<DebuffSkill> debuffs = new List<DebuffSkill>();
    public List<BuffSkill> buffs = new List<BuffSkill>();

    public Dictionary<int, Skill> MakeDict()
    {
        Dictionary<int, Skill> dict = new Dictionary<int, Skill>();
        foreach (RangeSkill skill in ranges)
        {
            dict.Add(skill.Id, skill);
        }
        foreach (TargetSkill target in targets)
        {
            dict.Add(target.Id, target);
        }
        foreach (DebuffSkill debuff in debuffs)
        {
            dict.Add(debuff.Id, debuff);
        }
        foreach (BuffSkill buff in buffs)
        {
            dict.Add(buff.Id, buff);
        }

        return dict;
    }
}

[Serializable]
public class Skill
{
    public int Id;
    public int Type;
    public string Name;
    public string Path;
    public float CoolTime;
}

[Serializable]
public class RangeSkill : Skill
{
    public int Damage;
}

[Serializable]
public class TargetSkill : Skill
{
    public int Damage;
}

[Serializable]
public class DebuffSkill : Skill
{
    public float Duration;
    public int Speed;
}

[Serializable]
public class BuffSkill : Skill
{
    public float Duration;
    public float AttackSpeed;
}
