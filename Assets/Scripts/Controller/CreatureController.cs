using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    protected enum CreatureState
    {
        Move,
        Attack,
    }
    protected CreatureState _state;

    protected int _maxHp;
    public float Speed { get; set; }
    protected int _attack;
    public int Hp { get; protected set; }

    public bool InSkillRange { get; protected set; } = false;

    protected float _checkTime = 0f;
    protected float _coolTime = 1f;
    protected bool _canAttack = true;

    protected RectTransform _transform;
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
    protected HpBar _hpBar;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _transform = GetComponent<RectTransform>();
        _hpBar = Utils.FindChild(gameObject, "HP").gameObject.GetComponent<HpBar>();
        if (_hpBar == null)
            Debug.Log("Failed to find HPBar");
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

    protected virtual void UpdateAnim()
    {

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
    protected virtual void OnDead()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RangeController>() != null)
            InSkillRange = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RangeController>() != null)
            InSkillRange = false;
    }
}
