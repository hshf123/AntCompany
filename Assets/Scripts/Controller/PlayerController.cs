using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    GameObject _stage;
    CreatureController _target;
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
                    _animator.speed = Managers.Game.TotalAttackSpeed;
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
        Patrol();
        if (_target != null && _canAttack)
            AttackStart();

        if (_target == null)
            State = PlayerState.IDLE;
    }

    void Patrol()
    {
        if (Managers.Game.Creatures.Count == 0)
        {
            _target = null;
            return;
        }

        _target = Managers.Game.Creatures[0];
        foreach (CreatureController cc in Managers.Game.Creatures)
        {
            if (_target != null)
            {
                if (_target.transform.position.y >= cc.transform.position.y)
                {
                    if (_target.transform.position.y <= _attackRange)
                        _target = cc;
                    else
                        _target = null;
                }
            }
        }
    }

    void AttackStart()
    {
        if (_checkTime >= (1 / Managers.Game.TotalAttackSpeed))
        {
            GameObject arrow = Managers.Resource.Instantiate("Objects/Arrow", _stage.transform);
            arrow.transform.position = gameObject.transform.position;
            _checkTime = 0;
            if (_target != null)
                arrow.GetComponent<ArrowController>().SetTarget(_target.transform.position);
            State = PlayerState.Attack;
            Managers.Sound.Play("Sound_AttackButton");
        }
    }
}
