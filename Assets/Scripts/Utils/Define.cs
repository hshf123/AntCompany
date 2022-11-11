using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum SkillType
    {
        Range,
        Target,
        Debuff,
        Buff,
    }

    public enum EquipmentType
    {
        None,
        Attack,
        AttackSpeed,
        MaxHp,
        CoolTimeReduce,
    }

    public enum Scene
    {
        Unknown,
        Test,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        Drag,
    }
}
