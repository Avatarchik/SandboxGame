using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : ThrowWeaponBase
{
    private WoodenBowView m_WoodenBowView;

    protected override void Init()
    {
        m_WoodenBowView = (WoodenBowView)M_GunViewBase;
        CanShoot(0);
    }

    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    protected override void Shoot()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_WoodenBowView.M_Arrow, m_WoodenBowView.M_GunPoint.position, m_WoodenBowView.M_GunPoint.rotation);
        go.GetComponent<Arrow>().Shoot(m_WoodenBowView.M_GunPoint.forward, 1000, Damage);
        //降低耐久
        Durable--;
    }
}
