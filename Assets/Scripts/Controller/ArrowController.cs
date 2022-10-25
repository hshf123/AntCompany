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
        _speed = 20f;
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        _dir = (target - gameObject.transform.position).normalized;
        // Set Rotation
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(_dir.y, _dir.x));
        // 90도를 빼는 이유는 좌표평면 위에서 X축을 기준으로 시작하기 때문이다.
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void Update()
    {
        if (_target != null)
            MoveToTargetDir();
    }

    void MoveToTargetDir()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.GetComponent<MonsterController>();
        if (mc == null)
            return;
        Managers.Resource.Destroy(gameObject);
        mc.OnDamaged(Managers.Game.Attack);
    }
}
