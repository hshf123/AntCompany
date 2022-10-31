using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    GameObject _stage;
    MonsterController _target;
    BossController _bossTarget;
    float _attackRange;
    float _checkTime = 0f;
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
                    _animator.Play("Player_Idle");
                    break;
                case PlayerState.Attack:
                    _animator.speed = Managers.Game.AttackSpeed;
                    _animator.Play("Player_Attack");
                    break;
            }

            _state = value;
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        State = PlayerState.IDLE;
        _stage = gameObject.transform.parent.gameObject;
        _attackRange = Screen.height;
    }

    void Update()
    {
        _checkTime += Time.deltaTime;
        FindMonster();
        FindBoss();
        if (_target != null && _canAttack)
            AttackStart();
        if (_bossTarget != null && _canAttack)
            AttackStart();

        if (_target == null && _bossTarget == null)
            State = PlayerState.IDLE;
    }

    void FindMonster()
    {
        if (Managers.Game.Monsters.Count == 0)
        {
            _target = null;
            return;
        }

        _target = Managers.Game.Monsters[0];
        foreach (MonsterController mc in Managers.Game.Monsters)
        {
            if (_target != null)
            {
                if (_target.transform.position.y >= mc.transform.position.y)
                {
                    if (_target.transform.position.y <= _attackRange)
                        _target = mc;
                    else
                        _target = null;
                }
            }
        }
    }
    void FindBoss()
    {
        if (Managers.Game.Boss == null)
        {
            _bossTarget = null;
            return;
        }

        _bossTarget = Managers.Game.Boss;

        if (_bossTarget.transform.position.y > _attackRange)
            _bossTarget = null;
    }

    void AttackStart()
    {
        if (_checkTime >= (1 / Managers.Game.AttackSpeed))
        {
            GameObject arrow = Managers.Resource.Instantiate("Objects/Arrow", _stage.transform);
            arrow.transform.position = gameObject.transform.position;
            _checkTime = 0;
            if (_target != null)
                arrow.GetComponent<ArrowController>().SetTarget(_target.transform.position);
            if (_bossTarget != null)
                arrow.GetComponent<ArrowController>().SetTarget(_bossTarget.transform.position);
            State = PlayerState.Attack;
            Managers.Sound.Play("Sound_AttackButton");
        }
    }
}
