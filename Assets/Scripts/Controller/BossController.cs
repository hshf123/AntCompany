using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : CreatureController
{
    Boss _bossData;

    SkeletonGraphic _animator;
    UI_BossStagePopup _stage;

    protected CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            switch (value)
            {
                case CreatureState.Move:
                    PlayAnimation("walk");
                    ChangeSkin("main");
                    break;
                case CreatureState.Attack:
                    PlayAnimation("attack");
                    ChangeSkin("attack");
                    break;
            }

            _state = value;
        }
    }

    protected override void Init()
    {
        base.Init();

        _animator = GetComponent<SkeletonGraphic>();
        PlayAnimation("walk");
        ChangeSkin("main");
        SetRandPosition();
        State = CreatureState.Move;

        if (Managers.Data.BossDict.TryGetValue(1, out _bossData) == false)
        {
            Debug.Log("Failed to load boss data");
            return;
        }

        _maxHp = _bossData.MaxHp;
        Hp = _bossData.Hp;
        _attack = _bossData.Attack;
        _speed = _bossData.Speed;

        Managers.Game.Creatures.Add(this);
    }

    public void PlayAnimation(string name, bool loop = true)
    {
        _animator.startingAnimation = name;
        _animator.startingLoop = loop;
    }

    public void ChangeSkin(string name)
    {
        _animator.initialSkinName = name;
        _animator.Initialize(true);
    }

    void SetRandPosition()
    {
        float xRange = gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2;
        float yRange = 200;
        float x = Random.Range(-xRange + 40, xRange - 40);
        float y = Random.Range(2000, 2000 + yRange + 1);
        Position = new Vector3(x, y);
    }

    protected override void UpdateAnim()
    {
        if (Position.y <= -515f)
        {
            State = CreatureState.Attack;
            Position = new Vector2(Position.x, -515f);
            if (_canAttack)
            {
                Managers.Game.OnDamaged(_attack);
                Managers.Sound.Play("Sound_Cancelbutton");
            }
        }
        else
        {
            State = CreatureState.Move;
            Position += new Vector2(0, -1f) * Time.deltaTime * _speed;
        }
    }

    public void SetStage(GameObject stage)
    {
        UI_BossStagePopup sp = stage.GetComponent<UI_BossStagePopup>();
        if (sp == null)
            return;

        _stage = sp;
    }

    protected override void OnDead()
    {
        Managers.Resource.Destroy(gameObject);
        //Managers.Game.Monsters.Remove(gameObject.GetComponent<MonsterController>());
        // TODO : º“∏Í ¿Ã∆Â∆Æ
    }

    
}
