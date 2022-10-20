using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;

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
        _animator.Play("Monster_Walk");
        State = PlayerState.IDLE;
    }

    void Update()
    {
        UpdateAnim();
    }

    void UpdateAnim()
    {
        // TODO : 몬스터 발견 시 공격
    }
}
