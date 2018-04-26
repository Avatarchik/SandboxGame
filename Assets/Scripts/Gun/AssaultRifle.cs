using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



public class AssaultRifle : MonoBehaviour {

    private AssaultRifleView m_AssaultRifleView;

    //字段
    private int id; //ID
    private int damage; //伤害
    private int durable; //耐久
    private GunType gunWeaponType;

    private AudioClipLoadType audio; //声音特效
    private GameObject Effect; //动画特效

    public int Id {
        get { return id; }
        set { id = value; }
    }
    public int Damage {
        get { return damage; }
        set { damage = value; }
    }
    public int Durable {
        get { return durable; }
        set { durable = value; }
    }
    public GunType GunWeaponType {
        get { return gunWeaponType; }
        set { gunWeaponType = value; }
    }

    public AudioClipLoadType Audio {
        get { return audio; }
        set { audio = value; }
    }

    public GameObject Effect1 {
        get { return Effect; }
        set { Effect = value; }
    }

    void Start () {
        init ();
    }

    void Update () {
        MouseControl ();
    }

    //初始化.
    private void init () {
        m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView> ();
    }

    private void PlayAudio () {
        Debug.Log ("播放音效");
    }

    private void PlayEffect () {
        Debug.Log ("播放特效");
    }

    //射击
    private void Shoot () {
        Debug.Log ("射击");
    }

    //鼠标控制
    private void MouseControl () {
        if (Input.GetMouseButtonDown (0)) //按下鼠标左键->射击.
        {
            m_AssaultRifleView.M_Animator.SetTrigger ("Fire");
        }

        if (Input.GetMouseButton (1)) //按住鼠标右键->开镜  
        {
            m_AssaultRifleView.M_Animator.SetBool ("HoldPose", true);
            m_AssaultRifleView.EnterHoldPos ();
        }
        if (Input.GetMouseButtonUp (1)) //松开鼠标右键，退出开镜    
        {
            m_AssaultRifleView.M_Animator.SetBool ("HoldPose", false);
            m_AssaultRifleView.ExitHoldPose ();
        }
    }

}