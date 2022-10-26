using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Monster monsterData;
    int _maxHp;
    int _hp;
    int _attack;
    float _speed;

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
    Animator _animator;
    UI_StagePopup _stage;
    HpBar _hpBar;

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
        _hpBar = Utils.FindChild(gameObject, "HP").gameObject.GetComponent<HpBar>();
        if (_hpBar == null)
            Debug.Log("Failed to find HPBar");

        if(Managers.Data.MonsterDict.TryGetValue((int)_stage.StageLevel, out monsterData)==false)
        {
            Debug.Log("Failed to load monster data");
            return;
        }

        _maxHp = monsterData.MaxHp;
        _hp = monsterData.Hp;
        _attack = monsterData.Attack;
        _speed = monsterData.Speed;
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
            {
                Managers.Game.OnDamaged(_attack);
                Managers.Sound.Play("Sound_Cancelbutton");
             }
        }
        else
        {
            State = MonsterState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
        }
    }

    public void OnDamaged(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
            _hp = 0;

        float ratio = (float)_hp / _maxHp;
        _hpBar.SetHpBar(ratio);
        Managers.Sound.Play("Sound_PlayerAttacked");

        if (_hp == 0)
            OnDead();
    }

    public void SetStage(GameObject stage)
    {
        UI_StagePopup sp = stage.GetComponent<UI_StagePopup>();
        if (sp == null)
            return;

        _stage = sp;
    }

    void OnDead()
    {
        Managers.Resource.Destroy(gameObject);
        _stage.Monsters.Remove(gameObject.GetComponent<MonsterController>());
        // TODO : º“∏Í ¿Ã∆Â∆Æ
    }
}
