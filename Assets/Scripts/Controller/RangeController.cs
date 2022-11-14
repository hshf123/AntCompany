using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static System.Net.WebRequestMethods;

public class RangeController : MonoBehaviour
{
    int _damage;

    void Start()
    {
        gameObject.BindEvent(MoveRange, Define.UIEvent.Pressed);
        gameObject.BindEvent(StartSkill, Define.UIEvent.PointerUp);
    }

    void MoveRange()
    {
        UI_EventHandler evt = gameObject.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }

    void StartSkill()
    {
        if (Managers.Game.Boss != null)
        {
            if (Managers.Game.Boss.InSkillRange == true)
            {
                Managers.Game.Boss.OnDamaged(_damage);
                Debug.Log($"SkillDamage {_damage}");
            }
        }

        for (int i = 0; i < Managers.Game.Monsters.Count; i++)
        {
            MonsterController mc = Managers.Game.Monsters[i];
            if (mc.InSkillRange && mc != null)
            {
                Managers.Game.Monsters[i].OnDamaged(_damage);
                Debug.Log($"SkillDamage {_damage}");
            }
        }

        Managers.Resource.Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
