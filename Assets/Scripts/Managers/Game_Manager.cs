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

    // TODO : 스킬정보

    // TODO : 장비 정보
    public int SlotNumber { get; set; } = 0;

    public Dictionary<int, Equipment> Wearing { get; private set; } = new Dictionary<int, Equipment>();

    // in Game
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
    public List<MonsterController> Monsters = new List<MonsterController>();
    public BossController Boss;

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

    void Update()
    {
        // TODO
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
            if (Level >= 5)
                Level = 5;
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

        switch ((Define.Equipment)equipment.Type)
        {
            case Define.Equipment.Attack:
                TotalAttack += (equipment as AttackEquipment).Attack;
                break;
            case Define.Equipment.AttackSpeed:
                TotalAttackSpeed += (equipment as AttackSpeedEquipment).AttackSpeed;
                break;
            case Define.Equipment.MaxHp:
                int maxHp = (equipment as MaxHpEquipment).MaxHp;
                TotalMaxHP += maxHp;
                TotalHP += maxHp;
                break;
            case Define.Equipment.CoolTimeReduce:
                // TODO 쿨타임 감소 적용
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

        Define.Equipment type = (Define.Equipment)equipment.Type;
        switch (type)
        {
            case Define.Equipment.Attack:
                TotalAttack -= (equipment as AttackEquipment).Attack;
                break;
            case Define.Equipment.AttackSpeed:
                TotalAttackSpeed -= (equipment as AttackSpeedEquipment).AttackSpeed;
                break;
            case Define.Equipment.MaxHp:
                int maxHp = (equipment as MaxHpEquipment).MaxHp;
                TotalMaxHP -= maxHp;
                TotalHP -= maxHp;
                break;
            case Define.Equipment.CoolTimeReduce:
                // TODO 쿨타임 감소 적용
                break;
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

        // TODO : 스킬 정보 세이브
        foreach (Equipment equipment in Wearing.Values)
        {
            save.Equipment.Add(equipment);
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

        if (save.Equipment.Count > 0)
        {
            int equipCount = 0;
            foreach (Equipment equipment in save.Equipment)
            {
                Define.Equipment type = (Define.Equipment)equipment.Type;
                switch (type)
                {
                    case Define.Equipment.Attack:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.Equipment.AttackSpeed:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.Equipment.MaxHp:
                        {
                            Equipment equip;
                            if (Managers.Data.EquipmentDict.TryGetValue(equipment.Id, out equip))
                                SelectEquipment(equipCount, equip);
                        }
                        break;
                    case Define.Equipment.CoolTimeReduce:
                        // TODO 쿨타임 감소 적용
                        break;
                }
                equipCount++;
            }
        }
        // TODO : 스킬 정보 세이브

        return true;
    }
}
