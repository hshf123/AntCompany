using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager
{
    // ½½·Ô¹øÈ£ 1~16
    public Dictionary<int, Skill> Skills { get; } = new Dictionary<int, Skill>();
    public Skill SelectedSkill { get; set; } = null;

    public void Add(Skill skill)
    {
        Skills.Add(skill.Id, skill);
    }

    public Skill Get(int id)
    {
        Skill skill = null;
        Skills.TryGetValue(id, out skill);
        return skill;
    }

    public Skill Find(Func<Skill, bool> condition)
    {
        foreach (Skill skill in Skills.Values)
        {
            if (condition.Invoke(skill))
                return skill;
        }

        return null;
    }

    public void Clear()
    {
        Skills.Clear();
    }
}
