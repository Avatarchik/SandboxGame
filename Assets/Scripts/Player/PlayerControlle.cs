using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour {

    [SerializeField]
    private Transform m_Transform;
    [SerializeField]
    //private GameObject m_BuildingPlan;  //建造角色
    //private GameObject m_WoodenSpear;   //长矛角色
    //private Animator m_Animator;

    private GameObject currentWeapon;   //当前武器
    private GameObject targetWeapon;    //目标武器

    //private bool isNormal = false; //true:空手 false:建造

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        //m_BuildingPlan = m_Transform.Find("PersonCamera/Building Plan").gameObject;
        //m_WoodenSpear = m_Transform.Find("PersonCamera/Wooden Spear").gameObject;

        //m_WoodenSpear.SetActive(false);
        //m_Animator = m_BuildingPlan.GetComponent<Animator>();
        //currentWeapon = m_BuildingPlan;
        targetWeapon = null;
    }
	
	void Update () {
        //切换到建造
		if(Input.GetKeyDown(KeyCode.M))
        {
            //targetWeapon = m_BuildingPlan;
            Changed();

        }
        //切换到长矛
        if (Input.GetKeyDown(KeyCode.K))
        {
            //targetWeapon = m_WoodenSpear;
            Changed();

        }
    }

    private void Changed()
    {
        /*if(isNormal)
        {
            //抬起当前的物品.
            m_BuildingPlan.SetActive(true);
            isNormal = false;
        }
        else
        {
            //放下当前的物品.
            m_Animator.SetTrigger("Holster");
            StartCoroutine("DelayTime");
            isNormal = true;
        }*/

        //放下当前武器
        currentWeapon.GetComponent<Animator>().SetTrigger("Holster");
        StartCoroutine("DelayTime");

    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1);
        //延迟1s,防止放下动画没有播放完就隐藏物体
        currentWeapon.SetActive(false);
        //抬起目标武器
        targetWeapon.SetActive(true);
        currentWeapon = targetWeapon;
    }
}