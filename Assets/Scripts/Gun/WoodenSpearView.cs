using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpearView : GunViewBase
{
    public GameObject spear;

    public GameObject M_Spear
    {
        get { return spear; }
    }

    protected override void FindGunPoint()
    {
        M_GunPoint = M_Transform.Find("Armature/Arm_R/Forearm_R/Wrist_R/Weapon/EffectPos_A");
    }

    protected override void Init()
    {
        spear = Resources.Load<GameObject>("Gun/Wooden_Spear");
    }

    protected override void InitHoldPoseValue()
    {
        M_StartPos = M_Transform.localPosition;
        M_StartRot = M_Transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0, -1.58f, 0.32f);
        M_EndRot = new Vector3(0.4f, 0.3f);
    }

}
