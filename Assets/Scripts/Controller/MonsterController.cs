using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    RectTransform _transform;
    public Vector2 Position
    {
        get { return _transform.anchoredPosition; }
        protected set
        {
            if (_transform.anchoredPosition == value)
                return;

            _transform.anchoredPosition = value;
        }
    }
    public float _speed = 200f;
    Animator _animator;

    protected enum MonsterState
    {
        Move,
        Attack,
    }
    MonsterState _state;
    protected MonsterState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            switch (value)
            {
                case MonsterState.Move:
                    _animator.Play("Monster_Walk");
                    break;
                case MonsterState.Attack:
                    _animator.Play("Monster_Attack");
                    break;
            }
        }
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        _animator.Play("Monster_Walk");
        Position = new Vector2(0, 2000);
        State = MonsterState.Move;
    }

    void Update()
    {
        UpdateAnim();
    }

    void UpdateAnim()
    {
        if (Position.y <= -426f)
        {
            // TODO : 공격
            State = MonsterState.Attack;
            Position = new Vector2(0, -426f);

        }
        else
        {
            // TODO : 이동
            State = MonsterState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;

        }
    }
}
