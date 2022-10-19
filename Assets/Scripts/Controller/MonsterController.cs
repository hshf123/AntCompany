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

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        _animator.Play("Monster_Walk");
        Position = new Vector2(0, 2000);
    }

    void Update()
    {
        Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
    }
}
