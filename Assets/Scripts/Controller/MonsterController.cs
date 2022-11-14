using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    Monster monsterData;

    Animator _animator;
    UI_StagePopup _stage;

    protected CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            switch (value)
            {
                case CreatureState.Move:
                    _animator.Play("Monster_Walk");
                    break;
                case CreatureState.Attack:
                    _animator.Play("Monster_Attack");
                    break;
            }

            _state = value;
        }
    }

    protected override void Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        _animator.Play("Monster_Walk");
        SetRandPosition();
        State = CreatureState.Move;

        if (Managers.Data.MonsterDict.TryGetValue((int)_stage.StageLevel, out monsterData) == false)
        {
            Debug.Log("Failed to load monster data");
            return;
        }

        _maxHp = monsterData.MaxHp;
        Hp = monsterData.Hp;
        _attack = monsterData.Attack;
        _speed = monsterData.Speed;
    }

    void SetRandPosition()
    {
        float xRange = gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2;
        float yRange = 200;
        float x = Random.Range(-xRange + 30, xRange - 30);
        float y = Random.Range(2000, 2000 + yRange + 1);
        Position = new Vector3(x, y);
    }

    protected override void UpdateAnim()
    {
        if (Position.y <= -426f)
        {
            State = CreatureState.Attack;
            Position = new Vector2(Position.x, -426f);
            if (_canAttack)
            {
                Managers.Game.OnDamaged(_attack);
                Managers.Sound.Play("Sound_Cancelbutton");
            }
        }
        else
        {
            State = CreatureState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
        }
    }

    public void SetStage(GameObject stage)
    {
        UI_StagePopup sp = stage.GetComponent<UI_StagePopup>();
        if (sp == null)
            return;

        _stage = sp;
    }

    protected override void OnDead()
    {
        Managers.Resource.Destroy(gameObject);
        Managers.Game.Creatures.Remove(gameObject.GetComponent<MonsterController>());
        // TODO : º“∏Í ¿Ã∆Â∆Æ
    }
}
