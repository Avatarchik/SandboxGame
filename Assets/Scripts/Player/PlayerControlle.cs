using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour {

    private Transform m_Transform;
    private GameObject m_BuildingPlan;
    private Animator m_Animator;

    private bool isNormal = false; //true:空手 false:建造

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_BuildingPlan = m_Transform.Find("Character/Building Plan").gameObject;
        m_Animator = m_BuildingPlan.GetComponent<Animator>();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
        {
            Chaged();

        }
	}

    private void Chaged()
    {
        if(isNormal)
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
        }
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1);
        //延迟1s,防止放下动画没有播放完就隐藏物体
        m_BuildingPlan.SetActive(false);
    }
}
