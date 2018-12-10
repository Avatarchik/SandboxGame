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
		m_navMeshAgent.SetDestination(dir);
		//m_Transform.LookAt(dir);
		
	}
	
	
	// Update is called once per frame
	void Update () {
		Distance();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Death();
		}
	}

	private void Distance()
	{
		if (Vector3.Distance(m_Transform.position, dir) <= 1)
		{
			int index = Random.Range(0, posList.Count);
			dir = posList[index];
			m_navMeshAgent.SetDestination(dir);
			//m_Transform.LookAt(dir);
		}
	}
	
	
	
	
	private void Death()
	{
		GameObject.Destroy(gameObject);
		SendMessageUpwards("AIDeath",gameObject);
	}
	
}
