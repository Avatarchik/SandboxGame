using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

	private Vector3 dir;
	private Transform m_Transform;
	private NavMeshAgent m_navMeshAgent;
	
	public Vector3 Dir
	{
		get { return dir; }
		set { dir = value; }
	}

	//AI位置
	private List<Vector3> posList = new List<Vector3>();
	public List<Vector3> PosList
	{
		get { return posList; }
		set { posList = value; }
	}
	
	void Start()
	{
		m_Transform = gameObject.GetComponent<Transform>();
		m_navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		m_navMeshAgent.SetDestination(dir);	//设置初始面向
		//m_Transform.LookAt(dir);
		
	}
	
	void Update () {
		Distance();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Death();
		}
	}

	private void Distance()
	{
		//到达目标位置
		if (Vector3.Distance(m_Transform.position, dir) <= 1)
		{
			//随机另一个目标位置
			int index = Random.Range(0, posList.Count);
			//更改带方向
			dir = posList[index];
			//重新寻路
			m_navMeshAgent.SetDestination(dir);
			//m_Transform.LookAt(dir);
		}
	}
	
	//AI死亡，销毁物体
	private void Death()
	{
		GameObject.Destroy(gameObject);
		SendMessageUpwards("AIDeath",gameObject);
	}
	
}
