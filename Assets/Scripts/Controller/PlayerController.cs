using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    GameObject _stage;
    public CreatureController Target { get; private set; }
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
        Managers.Game.Player = this;
    }

    void Update()
    {
        _checkTime += Time.deltaTime;
        Patrol();
        if (Target != null && _canAttack)
            AttackStart();

        if (Target == null)
            State = PlayerState.IDLE;
    }

    void Patrol()
    {
        if (Managers.Game.Creatures.Count == 0)
        {
            Target = null;
            return;
        }

        Target = Managers.Game.Creatures[0];
        foreach (CreatureController cc in Managers.Game.Creatures)
        {
            if (Target != null)
            {
                if (Target.transform.position.y >= cc.transform.position.y)
                {
                    if (Target.transform.position.y <= _attackRange)
                        Target = cc;
                    else
                        Target = null;
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
            if (Target != null)
                arrow.GetComponent<ArrowController>().SetTarget(Target.transform.position);
            State = PlayerState.Attack;
            Managers.Sound.Play("Sound_AttackButton");
        }
    }
}
