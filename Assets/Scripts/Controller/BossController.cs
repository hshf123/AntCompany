using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Boss _bossData;
    int _maxHp;
    public int Hp { get; private set; }
    int _attack;
    float _speed;

    public bool InSkillRange { get; private set; } = false;
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
    SkeletonGraphic _animator;
    UI_BossStagePopup _stage;
    HpBar _hpBar;

    protected enum BossState
    {
        Move,
        Attack,
    }
    BossState _state;
    protected BossState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            switch (value)
            {
                case BossState.Move:
                    PlayAnimation("walk");
                    break;
                case BossState.Attack:
                    PlayAnimation("attack");
                    break;
            }

            _state = value;
        }
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _animator = GetComponent<SkeletonGraphic>();
        PlayAnimation("walk");
        SetRandPosition();
        State = BossState.Move;
        _hpBar = Utils.FindChild(gameObject, "HP").gameObject.GetComponent<HpBar>();
        if (_hpBar == null)
            Debug.Log("Failed to find HPBar");

        if (Managers.Data.BossDict.TryGetValue(1, out _bossData) == false)
        {
            Debug.Log("Failed to load boss data");
            return;
        }

        _maxHp = _bossData.MaxHp;
        Hp = _bossData.Hp;
        _attack = _bossData.Attack;
        _speed = _bossData.Speed;

        Managers.Game.Boss = this;
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

    public void PlayAnimation(string name, bool loop = true)
    {
        _animator.startingAnimation = name;
        _animator.startingLoop = loop;
    }

    void SetRandPosition()
    {
        float xRange = gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2;
        float yRange = 200;
        float x = Random.Range(-xRange + 40, xRange - 40);
        float y = Random.Range(2000, 2000 + yRange + 1);
        Position = new Vector3(x, y);
    }

    void UpdateAnim()
    {
        if (Position.y <= -515f)
        {
            State = BossState.Attack;
            Position = new Vector2(Position.x, -515f);
            if (_canAttack)
            {
                Managers.Game.OnDamaged(_attack);
                Managers.Sound.Play("Sound_Cancelbutton");
            }
        }
        else
        {
            State = BossState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
        }
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            Hp = 0;

        float ratio = (float)Hp / _maxHp;
        _hpBar.SetHpBar(ratio);
        Managers.Sound.Play("Sound_PlayerAttacked");

        if (Hp == 0 && gameObject != null)
            OnDead();
    }

    public void SetStage(GameObject stage)
    {
        UI_BossStagePopup sp = stage.GetComponent<UI_BossStagePopup>();
        if (sp == null)
            return;

        _stage = sp;
    }

    void OnDead()
    {
        Managers.Resource.Destroy(gameObject);
        //Managers.Game.Monsters.Remove(gameObject.GetComponent<MonsterController>());
        // TODO : º“∏Í ¿Ã∆Â∆Æ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RangeController>() != null)
            InSkillRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RangeController>() != null)
            InSkillRange = false;
    }
}
