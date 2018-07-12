using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowView : GunViewBase
{
    private GameObject arrow;
    public GameObject M_Arrow
    {
        get { return arrow; }
    }

    public override void FindGunPoint()
    {
        M_GunPoint = M_Transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/EffectPos_A");
    }

    public override void Init()
    {
        arrow = Resources.Load<GameObject>("Gun/Arrow");
    }

    public override void InitHoldPoseValue()
    {
        M_StartPos = M_Transform.localPosition;
        M_StartRot = M_Transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0.75f, -1.2f, 0.22f);
        M_EndRot = new Vector3(2.5f, -8, 35);
    }
}
