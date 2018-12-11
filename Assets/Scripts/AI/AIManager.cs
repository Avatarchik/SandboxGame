using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum AIManagerType
{
	CANNIBAL,	//僵尸
	BOAR,		//小猪
	NULL
}

public class AIManager : MonoBehaviour
{

	private Transform m_Transform;
	private GameObject prefab_Cannibal;
	private GameObject prefab_Boar;
	private AIManagerType aiManegerType = AIManagerType.NULL;
	private List<GameObject> AIList = new List<GameObject>();
	private Transform[] posTransform; 
	//private Queue<Vector3> posQueue = new Queue<Vector3>();
	private List<Vector3> posList = new List<Vector3>();

	private int index = 0;
	
	public AIManagerType AIManagerType
	{
		get { return aiManegerType; }
		set { aiManegerType = value; }
	}
	
	void Start ()
	{
		m_Transform = gameObject.GetComponent<Transform>();
		prefab_Cannibal = Resources.Load<GameObject>("AI/Cannibal");
		prefab_Boar = Resources.Load<GameObject>("AI/Boar");
		posTransform = m_Transform.GetComponentsInChildren<Transform>(true);
		for (int i = 1; i < posTransform.Length; i++)
		{
			//存储位置
			//posQueue.Enqueue(posTransform[i].position);
			posList.Add(posTransform[i].position);
		}
		CreatAIByEnum();
	}

	//通过枚举创建AI
	private void CreatAIByEnum()
	{
		if (aiManegerType == global::AIManagerType.CANNIBAL)
		{
			CreatAI(prefab_Cannibal);
		}else if (aiManegerType == global::AIManagerType.BOAR)
		{
			CreatAI(prefab_Boar);
		}
	}
	
	//AI实例化
	private void CreatAI(GameObject prefab_AI)
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject ai = GameObject.Instantiate<GameObject>(prefab_AI, m_Transform.position, Quaternion.identity, m_Transform);
			//ai.GetComponent<AI>().Dir = posQueue.Dequeue();
			ai.GetComponent<AI>().Dir = posList[i];
			ai.GetComponent<AI>().PosList = posList;
			AIList.Add(ai);
		}
	}

	//销毁AI,短暂延迟后创建新AI
	private void AIDeath(GameObject ai)
	{
		AIList.Remove(ai);
		StartCoroutine("CreatOneAI");
	}

	//协程创建新AI
	private IEnumerator CreatOneAI()
	{
		GameObject ai = null;
		yield return new WaitForSeconds(5);	//5秒后创建
		switch (aiManegerType)
		{
			case global::AIManagerType.CANNIBAL:
				ai = GameObject.Instantiate<GameObject>(prefab_Cannibal, m_Transform.position, Quaternion.identity, m_Transform);
				break;
			case global::AIManagerType.BOAR:
				ai = GameObject.Instantiate<GameObject>(prefab_Boar, m_Transform.position, Quaternion.identity, m_Transform);
				break;
		}

		if (ai != null)
		{
			ai.GetComponent<AI>().Dir = posList[index];
			ai.GetComponent<AI>().PosList = posList;
			
			//更新AIList
			index++;
			index = index % posList.Count();
			AIList.Add(ai);
		}
	}
	
}
