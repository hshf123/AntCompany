using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StagePopup : UI_Popup
{
    // TODO : 데이터시트에서 받는걸로
    public int MonsterCount { get; protected set; } = 1;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        return true;
    }

    protected void CreateMonster()
    {
        // 몬스터를 생성, 생성된 몬스터는 벽쪽으로 다가간다, 벽에 도달하면 공격상태에 들어간다.
        for(int i=0; i<MonsterCount; i++)
        {
            UI_StagePopup monster = Managers.UI.MakeSubItem<UI_StagePopup>(gameObject.transform, "Monster");
            RectTransform pos = monster.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(0, 0);
        }
    }
}
