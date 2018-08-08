using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunView : GunViewBase
{
    private Transform effectPos;//特效挂点
    private AudioClip effectAudio;//特效声音
    private GameObject shell; //弹壳模型
    private GameObject bullet;//弹头模型

    public Transform M_EffectPos
    {
        get { return effectPos; }
    }
    public AudioClip M_EffectAudio
    {
        get { return effectAudio; }
    }
    public GameObject M_Shell
    {
        get { return shell; }
    }
    public GameObject M_Bullet
    {
        get
        {
            return bullet;
        }
    }

    protected override void Init()
    {
        effectPos = M_Transform.Find("Armature/Weapon/EffectPos_B");
        effectAudio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Pump");
        shell = Resources.Load<GameObject>("Gun/Shotgun_shell");
        bullet = Resources.Load<GameObject>("Gun/Shotgun_Bullet");
    }

    protected override void InitHoldPoseValue()
    {
        
        //优化开镜
        M_StartPos = M_Transform.localPosition;
        M_StartRot = M_Transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(-0.14f, -1.78f, -0.03f);
        //M_EndPos = new Vector3(-0.19f, -1.79f, 0.13f);
        //M_EndPos = new Vector3(-0.29f, -1.72f, -0.13f);
        //M_EndRot = new Vector3(8.79f, 0.694f, -5.405f);
        M_EndRot = new Vector3(0, 10, 0.25f);

    }

    protected override void FindGunPoint()
    {
        M_GunPoint = M_Transform.Find("Armature/Weapon/EffectPos_A");
    }

}

