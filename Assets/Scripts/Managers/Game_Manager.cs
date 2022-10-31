using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    // in Game
    public int MaxHP { get; private set; }
    public int HP { get; private set; }
    public float AttackSpeed { get; set; }
    public int Attack { get; set; }

    public float ArrowSpeed { get; set; }

    // TODO 몬스터 카운트는 어디서 받을지
    public int MonsterCount { get; set; }
    public List<MonsterController> Monsters = new List<MonsterController>();

    public void Init()
    {
        if(Managers.Data.PlayerDict.TryGetValue(1, out _playerData) == false)
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

        Save();
    }

    void Update()
    {
        // TODO
    }

    public void OnDamaged(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            HP = 0;
    }
    public void AddExp(float exp)
    {
        Exp += exp;
        if (MaxExp <= Exp)
        {
            // TODO
            Level += 1;
            if (Level >= 5)
                Level = 5;
            Exp -= MaxExp;
        }
        Save();
        LoadData();
    }

    public void Save()
    {
        Save save = new Save();
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

        Managers.Data.Save(save);
    }
    public bool LoadData()
    {
        Save save;
        if ((save = Managers.Data.LoadSaveData()) == null)
            return false;

        int level = save.Level;

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

        // TODO : 스킬 정보 세이브

        return true;
    }
}
