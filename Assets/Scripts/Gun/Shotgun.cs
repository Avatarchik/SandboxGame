using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunControllerBase
{
    private ShotgunView m_ShotgunView;

    public override void Init()
    {
        m_ShotgunView = (ShotgunView)M_GunViewBase;
    }

    public override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Fire");
    }

    public override void LoadEffectAsset()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/Shotgun_GunPoint_Effect");
    }

    public override void PlayEffect()
    {
        //枪口特效
        GameObject tempEffect = GameObject.Instantiate<GameObject>(Effect, M_GunViewBase.M_GunPoint.position, Quaternion.identity);
        tempEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayDestory(tempEffect,2)); //销毁生成特效
        //弹壳弹出特效
        GameObject  tempShell = GameObject.Instantiate<GameObject>(m_ShotgunView.M_Shell, m_ShotgunView.M_EffectPos.position, Quaternion.identity);
        tempShell.GetComponent<Rigidbody>().AddForce(m_ShotgunView.M_EffectPos.up * 70);
        StartCoroutine(DelayDestory(tempShell,5.0f));
    }

    public override void Shoot()
    {
        StartCoroutine("CreateBullet");
    }





    //延迟销毁
    IEnumerator DelayDestory(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(go);
    }


    private IEnumerator CreateBullet()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
            GameObject tempBullet = Instantiate(m_ShotgunView.M_Bullet, m_ShotgunView.M_GunPoint.position, Quaternion.identity);
            tempBullet.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.M_GunPoint.forward + offset, 3000, Damage/5);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void PlayEffectAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShotgunView.M_EffectAudio, m_ShotgunView.M_EffectPos.position);
    }
}
