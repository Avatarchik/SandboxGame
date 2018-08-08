using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpear : ThrowWeaponBase {

    private WoodenSpearView m_WoodenSpearView;

    protected override void Init()
    {
        m_WoodenSpearView = (WoodenSpearView)M_GunViewBase;
        CanShoot(0);
    }

    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    protected override void Shoot()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_WoodenSpearView.M_Spear, m_WoodenSpearView.M_GunPoint.position, m_WoodenSpearView.M_GunPoint.rotation);
        go.GetComponent<Arrow>().Shoot(m_WoodenSpearView.M_GunPoint.forward, 1500, Damage);
        //go.GetComponent<Transform>().Find("Model").GetComponent<Arrow>().Shoot(m_WoodenSpearView.M_GunPoint.forward, 1500, Damage);
        //降低耐久
        Durable--;
    }

}
