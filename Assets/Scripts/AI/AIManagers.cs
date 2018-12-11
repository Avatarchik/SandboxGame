using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManagers : MonoBehaviour
{

	private Transform m_Transform;
	private Transform[] points;
	
	void Start ()
	{
		m_Transform = gameObject.GetComponent<Transform>();
		points = m_Transform.GetComponentsInChildren<Transform>();
		CreatAIManager();
	}

	//给每个位置点添加AI管理器脚本
	private void CreatAIManager()
	{
		for (int i = 1; i < points.Length; i++)
		{
			if (i % 2 == 0)
			{
				points[i].gameObject.AddComponent<AIManager>().AIManagerType = AIManagerType.CANNIBAL;
				
			}
			else
			{
				points[i].gameObject.AddComponent<AIManager>().AIManagerType = AIManagerType.BOAR;
			}
		}
	}
}
