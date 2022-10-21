using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    float _checkTime = 0f;
    float _coolTime = 1f;
    bool _canAttack = true;
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

            _state = value;
        }
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        _animator.Play("Monster_Walk");
        SetRandPosition();
        State = MonsterState.Move;
    }

    void Update()
    {
        if ((_checkTime += Time.deltaTime) >= _coolTime)
        {
            _canAttack = true;
            _checkTime = 0;
        }
        else
            _canAttack = false;
        UpdateAnim();
    }

    void SetRandPosition()
    {
        float xRange = gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2;
        float yRange = 200;
        float x = Random.Range(-xRange + 30, xRange - 30);
        float y = Random.Range(2000, 2000 + yRange + 1);
        Position = new Vector3(x, y);
    }

    void UpdateAnim()
    {
        if (Position.y <= -426f)
        {
            State = MonsterState.Attack;
            Position = new Vector2(Position.x, -426f);
            if (_canAttack)
                Managers.Game.HP -= 5;
        }
        else
        {
            State = MonsterState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
        }
    }
}
