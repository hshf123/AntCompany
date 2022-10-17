using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager
{
    public string NickName { get; private set; }
    public int Level { get; private set; }
    public int Money { get; private set; }

    public void Init()
    {
        NickName = "LeafC";
        Level = 1;
        Money = 10000;
    }
}
