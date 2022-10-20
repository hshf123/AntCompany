using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    GameObject _stage;
    MonsterController _target;
    float _attackRange;
    float _checkTime = 0f;
    float _coolTime = 1f;
    bool _canAttack = true;

    protected enum PlayerState
    {
        IDLE,
        Attack,
    }
    PlayerState _state;
    protected PlayerState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            switch (value)
            {
                case PlayerState.IDLE:
                    _animator.Play("PlayerIdle");
                    break;
                case PlayerState.Attack:
                    _animator.Play("PlayerAttack");
                    break;
            }

            _state = value;
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("PlayerIDLE");
        State = PlayerState.IDLE;
        _stage = gameObject.transform.parent.gameObject;
        _attackRange = Screen.height;
    }

    void Update()
    {
        _checkTime += Time.deltaTime;
        // TODO : 몬스터 발견
        FindMonster();
        // TODO : 몬스터 한테 화살 날림
        if (_target != null && _canAttack)
            AutoAttack();
    }

    void FindMonster()
    {
        UI_StagePopup stage = _stage.GetComponent<UI_StagePopup>();
        _target = stage.Monsters[0];
        foreach (MonsterController mc in stage.Monsters)
        {
            if (_target.transform.position.y >= mc.transform.position.y && _target != null)
            {
                if (_target.transform.position.y <= _attackRange)
                    _target = mc;
                else
                    _target = null;
            }
        }
    }

    void AutoAttack()
    {
        if (_checkTime >= _coolTime)
        {
            UI_StagePopup arrow = Managers.UI.MakeSubItem<UI_StagePopup>(_stage.transform, "Arrow");
            arrow.transform.position = gameObject.transform.position;
            _checkTime = 0;
            arrow.GetComponent<ArrowController>().SetTarget(_target.transform.position);
        }
    }
}
