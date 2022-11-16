using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class Game_Manager
{
    Player _playerData;
    Arrow _arrowData;

    // Main 정보
    public string Name { get; set; }
    public int Level { get; set; }
    public float MaxExp { get; set; }
    public float Exp { get; set; }
    public int Money { get; set; }

    // 스킬 정보
    public int SkillSlotNumber { get; set; } = 0;
    public Dictionary<int, Skill> Skills { get; private set; } = new Dictionary<int, Skill>();

    // 장비 정보
    public int EquipmentSlotNumber { get; set; } = 0;
    public Dictionary<int, Equipment> Wearing { get; private set; } = new Dictionary<int, Equipment>();

    // in Game
    public PlayerController Player { get; set; }
    public int TotalMaxHP { get; private set; } = 0;
    public int TotalHP { get; private set; } = 0;
    public float TotalAttackSpeed { get; set; } = 0;
    public int TotalAttack { get; set; } = 0;

    int MaxHP { get; set; }
    int HP { get; set; }
    float AttackSpeed { get; set; }
    int Attack { get; set; }

    public float ArrowSpeed { get; set; }

    public int MonsterCount { get; set; }
    public List<CreatureController> Creatures = new List<CreatureController>();

    public void Init()
    {
        if (Managers.Data.PlayerDict.TryGetValue(1, out _playerData) == false)
        {
            Debug.Log("Failed to load player data");
            return;
        }
        if (Managers.Data.ArrowDict.TryGetValue(1, out _arrowData) == false)
        {
            Debug.Log("Failed to load arrow data");
            return;
        }

        Level = _playerData.Level;
        MaxExp = 100;
        Exp = 0;
        Money = 100000;
        MaxHP = _playerData.MaxHp;
        HP = _playerData.Hp;
        Attack = _playerData.Attack;
        AttackSpeed = _playerData.AttackSpeed;

        ArrowSpeed = _arrowData.Speed;

        TotalAttack = Attack;
        TotalAttackSpeed = AttackSpeed;
        TotalMaxHP = MaxHP;
        TotalHP = HP;

        Save();
    }

    public void OnDamaged(int damage)
    {
        TotalHP -= damage;
        if (TotalHP <= 0)
            TotalHP = 0;
    }
    public void AddExp(float exp)
    {
        Exp += exp;
        CheckLevelUp();
        Save();
        LoadData();
    }
    void CheckLevelUp()
    {
        if (MaxExp <= Exp)
        {
            Exp -= MaxExp;
            Level += 1;
            if (Level >= Managers.Data.PlayerDict.Count)
                Level = Managers.Data.PlayerDict.Count;
            if (Exp >= MaxExp)
                CheckLevelUp();
        }
    }

    public void SelectEquipment(int slot, Equipment equipment)
    {
        if (equipment == null)
        {
            Debug.Log($"Equipment is Null");
            return;
        }

        Equipment checkEquip;
        if (Wearing.TryGetValue(slot, out checkEquip) == true)
        {
            ClearEquipment(slot);
            Wearing.Remove(slot);
        }
        Wearing.Add(slot, equipment);
        Managers.Inven.SelectedItem = null;

        switch ((Define.EquipmentType)equipment.Type)
        {
            case Define.EquipmentType.Attack:
                TotalAttack += (equipment as AttackEquipment).Attack;
                break;
            case Define.EquipmentType.AttackSpeed:
                TotalAttackSpeed += (equipment as AttackSpeedEquipment).AttackSpeed;
                break;
            case Define.EquipmentType.MaxHp:
                int maxHp = (equipment as MaxHpEquipment).MaxHp;
                TotalMaxHP += maxHp;
                TotalHP += maxHp;
                break;
            case Define.EquipmentType.CoolTimeReduce:
                float coolTimeReduce = 1 - (equipment as CoolTimeReduceEquipment).CoolTimeReduce;
                ReduceCoolTime(coolTimeReduce);
                break;
        }
    }
    public void ClearEquipment(int slot)
    {
        Equipment equipment;
        if (Wearing.TryGetValue(slot, out equipment) == false)
        {
            Debug.Log("No equipment in that slot");
            return;
        }

        Define.EquipmentType type = (Define.EquipmentType)equipment.Type;
        switch (type)
        {
            case Define.EquipmentType.Attack:
                TotalAttack -= (equipment as AttackEquipment).Attack;
                break;
            case Define.EquipmentType.AttackSpeed:
                TotalAttackSpeed -= (equipment as AttackSpeedEquipment).AttackSpeed;
                break;
            case Define.EquipmentType.MaxHp:
                int maxHp = (equipment as MaxHpEquipment).MaxHp;
                TotalMaxHP -= maxHp;
                TotalHP -= maxHp;
                break;
            case Define.EquipmentType.CoolTimeReduce:
                float coolTimeReduce = 1 - (equipment as CoolTimeReduceEquipment).CoolTimeReduce;
                RollbackCoolTime(coolTimeReduce);
                break;
        }
    }

    public void SelectSkill(int slot, Skill skill)
    {
        if (skill == null)
        {
            Debug.Log($"Skill is Null");
            return;
        }

        Skill checkSkill;
        if (Skills.TryGetValue(slot, out checkSkill) == true)
        {
            ClearSkill(slot);
            Skills.Remove(slot);
        }
        Skills.Add(slot, skill);
        Managers.Skill.SelectedSkill = null;

        switch ((Define.SkillType)skill.Type)
        {
            case Define.SkillType.Range:
                //(skill as RangeSkill).Damage;
                break;
            case Define.SkillType.Target:
                //(skill as TargetSkill);
                break;
            case Define.SkillType.Debuff:
                //(skill as DebuffSkill);
                break;
            case Define.SkillType.Buff:
                //(skill as BuffSkill);
                break;
        }
    }
    public void ClearSkill(int slot)
    {
        Skill skill;
        if (Skills.TryGetValue(slot, out skill) == false)
        {
            Debug.Log("No skill in that slot");
            return;
        }

        Define.SkillType type = (Define.SkillType)skill.Type;
        switch (type)
        {
            case Define.SkillType.Range:
                //(skill as AttackEquipment);
                break;
            case Define.SkillType.Target:
                //(skill as AttackSpeedEquipment);
                break;
            case Define.SkillType.Debuff:
                //(skill as MaxHpEquipment);
                break;
            case Define.SkillType.Buff:
                //(skill as BuffSkill);
                break;
        }
    }
    void ReduceCoolTime(float coolTimeReduce)
    {
        foreach(Skill skill in Skills.Values)
        {
            Debug.Log("ReduceCoolTime");
            Debug.Log($"{skill.Name} Original CoolTime : {skill.CoolTime}");
            skill.CoolTime *= coolTimeReduce;
            Debug.Log($"{skill.Name} Reduce CoolTime : {skill.CoolTime}");
        }
    }
    void RollbackCoolTime(float coolTimeReduce)
    {
        foreach (Skill skill in Skills.Values)
        {
            Debug.Log("RollbackCoolTime");
            Debug.Log($"{skill.Name} Reduce CoolTime : {skill.CoolTime}");
            skill.CoolTime /= coolTimeReduce;
            Debug.Log($"{skill.Name} Original CoolTime : {skill.CoolTime}");
        }
    }

    public void Save()
    {
        SaveData save = new SaveData();
        save.Name = Name;
        save.Level = Level;
        save.MaxExp = MaxExp;
        save.Exp = Exp;
        save.MaxHp = MaxHP;
        save.Hp = HP;
        save.Attack = Attack;
        save.AttackSpeed = AttackSpeed;
        save.Money = Money;

        foreach (Equipment equipment in Wearing.Values)
        {
            save.Equipment.Add(equipment);
        }

        foreach (Skill skill in Skills.Values)
        {
            save.Skill.Add(skill);
        }

        Managers.Data.Save(save);
    }
    public bool LoadData()
    {
        SaveData save;
        if ((save = Managers.Data.LoadSaveData()) == null)
            return false;

        int level = save.Level;

        TotalAttack -= Attack;
        TotalAttackSpeed -= AttackSpeed;
        TotalMaxHP -= MaxHP;

        if (Managers.Data.PlayerDict.TryGetValue(level, out _playerData) == false)
        {
            Debug.Log("Failed to load player data");
            return false;
        }
        if (Managers.Data.ArrowDict.TryGetValue(1, out _arrowData) == false)
        {
            Debug.Log("Failed to load arrow data");
            return false;
        }

        MaxExp = 100;
        MaxHP = _playerData.MaxHp;
        HP = _playerData.Hp;
        Attack = _playerData.Attack;
        AttackSpeed = _playerData.AttackSpeed;

        ArrowSpeed = _arrowData.Speed;

        Name = save.Name;
        Level = save.Level;
        Exp = save.Exp;
        Money = save.Money;

        TotalAttack += Attack;
        TotalAttackSpeed += AttackSpeed;
        TotalMaxHP += MaxHP;
        TotalHP = TotalMaxHP;

        if (save.Skill.Count > 0)
        {
            int skillCount = 0;
            foreach (Skill skill in save.Skill)
            {
                Define.SkillType type = (Define.SkillType)skill.Type;
                switch (type)
                {
                    case Define.SkillType.Range:
                        {
                            Skill s;
                            if (Managers.Data.SkillDict.TryGetValue(skill.Id, out s))
                                SelectSkill(skillCount, s);
                        }
                        break;
                    case Define.SkillType.Target:
                        {
                            Skill s;
                            if (Managers.Data.SkillDict.TryGetValue(skill.Id, out s))
                                SelectSkill(skillCount, s);
                        }
                        break;
                    case Define.SkillType.Debuff:
                        {
                            Skill s;
                            if (Managers.Data.SkillDict.TryGetValue(skill.Id, out s))
                                SelectSkill(skillCount, s);
                        }
                        break;
                    case Define.SkillType.Buff:
                        {
                            Skill s;
                            if (Managers.Data.SkillDict.TryGetValue(skill.Id, out s))
                                SelectSkill(skillCount, s);
                        }
                        break;
                }
                skillCount++;
            }
        }
        if (save.Equipment.Count > 0)
        {
            int equipCount = 0;
            foreach (Equipment equipment in save.Equipment)
            {
                Define.EquipmentType type = (Define.EquipmentType)equipment.Type;
                switch (type)
                {
                    case Define.EquipmentType.Attack:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.EquipmentType.AttackSpeed:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.EquipmentType.MaxHp:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.EquipmentType.CoolTimeReduce:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                }
                equipCount++;
            }
        }

        return true;
    }
}
