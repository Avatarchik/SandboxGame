using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



public class AssaultRifle : GunWeaponBase {

    private AssaultRifleView m_AssaultRifleView;
    
    private ObjectPool[] pools;

    protected override void PlayEffect () {
        GunEffect();
        ShellEffect();
    }

    //枪口特效
    private void GunEffect()
    {
        GameObject gunEffect = null;
        if (pools[0].Data())
        {
            gunEffect = pools[0].GetObject();
            gunEffect.GetComponent<Transform>().position = m_AssaultRifleView.M_GunPoint.position;
        }
        else
        {
            //Debug.Log ("播放特效");
            gunEffect = GameObject.Instantiate<GameObject>(Effect, m_AssaultRifleView.M_GunPoint.position, Quaternion.identity, m_AssaultRifleView.M_EffectParent);
            gunEffect.name = "GunPoint_Effect";
        }
        gunEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Delay(pools[0], gunEffect, 1));
    }

    //弹壳特效
    private void ShellEffect()
    {
        /*
        //弹壳弹出特效
        GameObject shell = null;
        if (pools[1].Data())
        {

            shell = pools[1].GetObject();
            shell.GetComponent<Rigidbody>().isKinematic = true;
            shell.GetComponent<Transform>().position = m_AssaultRifleView.M_EffectPos.position;
            shell.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            shell = GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Shell, m_AssaultRifleView.M_EffectPos.position, Quaternion.identity, m_AssaultRifleView.M_ShellParent);
            shell.name = "Shell";
        }
        shell.GetComponent<Rigidbody>().AddForce(m_AssaultRifleView.M_EffectPos.up * 50);
        StartCoroutine(Delay(pools[1], shell, 3));
        */
    }

    //射击
    protected override void Shoot () {
        //Debug.Log ("射击");
        if(Hit.point != Vector3.zero)
        {
            if(Hit.collider.GetComponent<BulletMark>()!=null)
            {
                Hit.collider.GetComponent<BulletMark>().CreateBulletMark(Hit);
                Hit.collider.GetComponent<BulletMark>().Hp -= Damage;
            }
            else{
            GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Bullet, Hit.point, Quaternion.identity);
            Debug.Log("生成子弹");
            }
        }
        else{
            Debug.Log("无子弹");
        }

        //降低耐久
        Durable--;
    }

    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
    }

    protected override void LoadEffectAsset()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");

    }

    //初始化
    protected override void Init()
    {
        //m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView> ();
        m_AssaultRifleView = (AssaultRifleView)M_GunViewBase;

        pools = gameObject.GetComponents<ObjectPool>();
    }
}