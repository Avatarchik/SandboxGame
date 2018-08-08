using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AssaultRifleView : GunViewBase {


	private Transform effectPos;//特效挂点
	private GameObject bullet;//临时子弹
	private GameObject shell;//弹壳

	//父物体
	private Transform effectParent;	//特效父物体
	private Transform shellParent;	//弹壳父物体
	
	public GameObject M_Bullet{
		get { return bullet; }
	}

	public Transform M_EffectPos{
		get {return effectPos;}
	}
	public GameObject M_Shell{
		get{return shell;}
	}

	public Transform M_EffectParent{
		get{ return effectParent; }
	}
	public Transform M_ShellParent{
		get{ return shellParent; }
	}

    protected override void FindGunPoint()
    {
        M_GunPoint = M_Transform.Find("Assault_Rifle/EffectPos_A");
    }

    protected override void InitHoldPoseValue()
    {
        //优化开镜
        M_StartPos = M_Transform.localPosition;
        M_StartRot = M_Transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(-0.065f, -1.85f, 0.25f);
        M_EndRot = new Vector3(2.8f, 1.3f, 0.08f);
    }

    /*
    public override void Awake()
    {
        base.Awake();

       
    }*/

    protected override void Init()
    {
        effectPos = M_Transform.Find("Assault_Rifle/EffectPos_B");

        bullet = Resources.Load<GameObject>("Gun/Bullet");
        shell = Resources.Load<GameObject>("Gun/Shell");

        effectParent = GameObject.Find("TempObject/AssaultRifle_Effect_Parent").GetComponent<Transform>();
        shellParent = GameObject.Find("TempObject/AssaultRifle_Shell_Parent").GetComponent<Transform>();

    }
}