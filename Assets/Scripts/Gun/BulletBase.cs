using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹抽象父类
/// </summary>
public abstract class BulletBase : MonoBehaviour {
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;

    private int damage;

    public Transform M_Transform
    {
        get { return m_Transform; }
    }
    public Rigidbody M_Rigidbody
    {
        get { return m_Rigidbody; }
    }
    public int M_Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }

    //尾部颤动
    private IEnumerator TailAnimation(Transform m_Pivot)
    {
        //动画执行时长
        float stopTime = Time.time + 1.0f;
        //动画颤动范围
        float range = 1.0f;
        //插值变化速度
        float vel = 0;
        //长矛动画开始的角度
        Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0));

        while (Time.time < stopTime)
        {
            //动画核心
            m_Pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0)) * startRot;
            //平滑阻尼（数学插值）
            range = Mathf.SmoothDamp(range, 0, ref vel, stopTime - Time.time);

            yield return null;
        }
    }

    public void KillSelf()
    {
        GameObject.Destroy(gameObject);
    }

    public abstract void Init();
    public abstract void Shoot(Vector3 dir, int force, int damage);
    public abstract void CollisionEnter(Collision collision);
}
