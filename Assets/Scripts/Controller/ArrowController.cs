using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Vector3 _target;
    Vector3 _dir;
    float _speed;

    void Start()
    {
        // TODO : 데이터 시트에서 가져올 것
        _speed = 10f;
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        _dir = (target - gameObject.transform.position).normalized;
    }

    void Update()
    {
        if (_target != null)
            MoveToTargetDir();
    }

    void MoveToTargetDir()
    {
        // TODO : 몬스터 피격판정
        if (CheckPosRange(gameObject.transform.position) == false)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }

        gameObject.transform.position += _dir * _speed;
    }

    bool CheckPosRange(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;
        if (x < 0 || x > Screen.width)
            return false;
        if (y < 0 || y > Screen.height)
            return false;
        return true;
    }
}
