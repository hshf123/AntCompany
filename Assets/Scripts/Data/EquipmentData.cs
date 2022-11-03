using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentData : ILoader<Define.Equipment, Equipment>
{
    public List<AttackEquipment> attacks = new List<AttackEquipment>();
    public List<AttackSpeedEquipment> attackSpeeds = new List<AttackSpeedEquipment>();
    public List<MaxHpEquipment> maxHps = new List<MaxHpEquipment>();
    public List<CoolTimeReduceEquipment> coolTimeReduces = new List<CoolTimeReduceEquipment>();

    public Dictionary<Define.Equipment, Equipment> MakeDict()
    {
        Dictionary<Define.Equipment, Equipment> dict = new Dictionary<Define.Equipment, Equipment>();
        foreach (AttackEquipment attack in attacks)
        {
            dict.Add((Define.Equipment)attack.Id, attack);
        }
        foreach (AttackSpeedEquipment attackSpeed in attackSpeeds)
        {
            dict.Add((Define.Equipment)attackSpeed.Id, attackSpeed);
        }
        foreach (MaxHpEquipment maxHp in maxHps)
        {
            dict.Add((Define.Equipment)maxHp.Id, maxHp);
        }
        foreach (CoolTimeReduceEquipment coolTimeReduce in coolTimeReduces)
        {
            dict.Add((Define.Equipment)coolTimeReduce.Id, coolTimeReduce);
        }

        return dict;
    }
}

[Serializable]
public class Equipment
{
    public int Id;
    public string Name;
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
