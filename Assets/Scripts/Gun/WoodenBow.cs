using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : GunControllerBase
{
    private WoodenBowView m_WoodenBowView;

    public override void Init()
    {
        m_WoodenBowView = (WoodenBowView)M_GunViewBase;
        CanShoot(0);
    }

    public override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    public override void LoadEffectAsset()
    {
        //弓箭无特效
    }

    public override void PlayEffect()
    {
        //弓箭无特效
    }

    public override void Shoot()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_WoodenBowView.M_Arrow, m_WoodenBowView.M_GunPoint.position, m_WoodenBowView.M_GunPoint.rotation);
        go.GetComponent<Arrow>().Shoot(m_WoodenBowView.M_GunPoint.forward, 1000, Damage);
    }
}
