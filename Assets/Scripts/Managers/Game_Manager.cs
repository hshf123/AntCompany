using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager
{
    // Main 정보
    public string NickName { get; private set; }
    public int Level { get; private set; }
    public float Exp { get; set; }
    public int Money { get; private set; }

    // TODO : 스킬정보

    // in Game
    public int MaxHP { get; private set; }
    public int HP { get; private set; }
    public float AttackSpeed { get; set; }
    public int Attack { get; set; }

    public void Init()
    {
        NickName = "LeafC";
        Level = 1;
        Money = 10000;

        // TEMP
        MaxHP = 1500;
        HP = 1500;
        AttackSpeed = 1f;
        Attack = 10;
    }

    void Update()
    {

    }

    public void OnDamaged(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            HP = 0;
    }
}
