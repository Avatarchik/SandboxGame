using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 枪械View层父类
/// </summary>
public abstract class GunViewBase : MonoBehaviour {

    //基础组件
    private Transform m_Transform;
    private Animator m_Animator;
    private Camera m_EnvCamera;

    //优化开镜动作
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 endPos;
    private Vector3 endRot;

    private Transform gunStar; //准星UI
    private Transform gunPoint;//枪口

    //基础组件属性
    public Transform M_Transform
    {
        get { return m_Transform; }
    }
    public Animator M_Animator
    {
        get { return m_Animator; }
    }
    public Camera M_EnvCamera
    {
        get { return m_EnvCamera; }
    }

    //开镜属性
    public Vector3 M_StartPos
    {
        get
        {
            return startPos;
        }

        set
        {
            startPos = value;
        }
    }
    public Vector3 M_StartRot
    {
        get
        {
            return startRot;
        }

        set
        {
            startRot = value;
        }
    }
    public Vector3 M_EndPos
    {
        get
        {
            return endPos;
        }

        set
        {
            endPos = value;
        }
    }
    public Vector3 M_EndRot
    {
        get
        {
            return endRot;
        }

        set
        {
            endRot = value;
        }
    }

    public Transform M_Gunstar
    {
        get { return gunStar; }
    }
    public Transform M_GunPoint
    {
        get { return gunPoint; }
        set { gunPoint = value;  }
    }

    public virtual void Awake()
    {
        //基础组件查找
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
        gunStar = GameObject.Find("GunStar").GetComponent<Transform>();

        InitHoldPoseValue();
        FindGunPoint();

        Init();
    }


    //进入开镜--动作优化
    public void EnterHoldPose(float time = 0.2f, int fov = 40)
    {
        M_Transform.DOLocalMove(M_EndPos, time);
        M_Transform.DOLocalRotate(M_EndRot, time);
        M_EnvCamera.DOFieldOfView(fov, 0.2f);
    }

    //退出开镜--动作优化
    public void ExitHoldPose(float time = 0.2f, int fov = 60)
    {
        M_Transform.DOLocalMove(M_StartPos, time);
        M_Transform.DOLocalRotate(M_StartRot, time);
        M_EnvCamera.DOFieldOfView(fov, time);
    }

    public abstract void Init();
    //初始化开镜动作相关的4个字段值
    public abstract void InitHoldPoseValue();
    //查找枪口
    public abstract void FindGunPoint();


}
