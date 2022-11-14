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
        for (int i = 0; i < Managers.Game.Creatures.Count; i++)
        {
            CreatureController cc = Managers.Game.Creatures[i];
            if (cc.InSkillRange && cc != null)
            {
                Managers.Game.Creatures[i].OnDamaged(_damage);
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
