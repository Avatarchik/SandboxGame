using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BulletBase
{

    private BoxCollider m_BoxCollider;
    private Transform m_Pivot;

    public override void Init()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();

        m_Pivot = M_Transform.Find("Pivot").GetComponent<Transform>();
    }

    public override void Shoot(Vector3 dir, int force, int damage)
    {
        M_Rigidbody.AddForce(dir * force);
        this.M_Damage = damage;
    }

    public override void CollisionEnter(Collision collision)
    {
        M_Rigidbody.Sleep();
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            //Debug.Log("碰到障碍");
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);
            collision.collider.GetComponent<BulletMark>().Hp -= M_Damage;
            M_Transform.SetParent(collision.collider.gameObject.transform);
            StartCoroutine("TailAnimation", m_Pivot);
        }
    }
} 