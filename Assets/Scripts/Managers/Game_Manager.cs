using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager
{
    // Main ����
    public string NickName { get; private set; }
    public int Level { get; private set; }
    public float Exp { get; set; }
    public int Money { get; private set; }

    // TODO : ��ų����

    // in Game
    public int HP { get; set; }
    public float AttackSpeed { get; set; }
    public int Attack { get; set; }

    public void Init()
    {
        NickName = "LeafC";
        Level = 1;
        Money = 10000;

        HP = 1500;
        AttackSpeed = 1f;
        Attack = 10;
    }

    void Update()
    {
        // TODO : ���� �ǰ� ����, ���� ���� ����

    }
}
