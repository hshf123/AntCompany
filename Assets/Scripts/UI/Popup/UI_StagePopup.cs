using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StagePopup : UI_Popup
{
    // TODO : �����ͽ�Ʈ���� �޴°ɷ�
    public int MonsterCount { get; protected set; } = 1;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        return true;
    }

    protected void CreateMonster()
    {
        // ���͸� ����, ������ ���ʹ� �������� �ٰ�����, ���� �����ϸ� ���ݻ��¿� ����.
        for(int i=0; i<MonsterCount; i++)
        {
            UI_StagePopup monster = Managers.UI.MakeSubItem<UI_StagePopup>(gameObject.transform, "Monster");
            RectTransform pos = monster.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(0, 0);
        }
    }
}
