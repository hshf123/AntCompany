using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentData : ILoader<int, Equipment>
{
    public List<AttackEquipment> attacks = new List<AttackEquipment>();
    public List<AttackSpeedEquipment> attackSpeeds = new List<AttackSpeedEquipment>();
    public List<MaxHpEquipment> maxHps = new List<MaxHpEquipment>();
    public List<CoolTimeReduceEquipment> coolTimeReduces = new List<CoolTimeReduceEquipment>();

    public Dictionary<int, Equipment> MakeDict()
    {
        Dictionary<int, Equipment> dict = new Dictionary<int, Equipment>();
        foreach (AttackEquipment attack in attacks)
        {
            dict.Add(attack.Id, attack);
        }
        foreach (AttackSpeedEquipment attackSpeed in attackSpeeds)
        {
            dict.Add(attackSpeed.Id, attackSpeed);
        }
        foreach (MaxHpEquipment maxHp in maxHps)
        {
            dict.Add(maxHp.Id, maxHp);
        }
        foreach (CoolTimeReduceEquipment coolTimeReduce in coolTimeReduces)
        {
            dict.Add(coolTimeReduce.Id, coolTimeReduce);
        }

        return dict;
    }
}

[Serializable]
public class Equipment
{
    public int Id;
    public int Type;
    public string Name;
    public string Path;
}

[Serializable]
public class AttackEquipment : Equipment
{
    public int Attack;
}

[Serializable]
public class AttackSpeedEquipment : Equipment
{
    public float AttackSpeed;
}

[Serializable]
public class MaxHpEquipment : Equipment
{
    public int MaxHp;
}

[Serializable]
public class CoolTimeReduceEquipment : Equipment
{
    public float CoolTimeReduce;
}
