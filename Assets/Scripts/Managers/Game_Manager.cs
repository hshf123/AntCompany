using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager
{
    Player _playerData;
    Arrow _arrowData;

    // Main ����
    public string Name { get; set; }
    public int Level { get; set; }
    public float MaxExp { get; set; }
    public float Exp { get; set; }
    public int Money { get; set; }

    // TODO : ��ų����

    // in Game
    public int MaxHP { get; private set; }
    public int HP { get; private set; }
    public float AttackSpeed { get; set; }
    public int Attack { get; set; }

    public float ArrowSpeed { get; set; }

    public void Init()
    {
        // TODO : ������ ���缭 ������ �ε�
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
        MaxHP = _playerData.MaxHp;
        HP = _playerData.Hp;
        Attack = _playerData.Attack;
        AttackSpeed = _playerData.AttackSpeed;

        ArrowSpeed = _arrowData.Speed;
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
}
