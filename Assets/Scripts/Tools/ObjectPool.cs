using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//对象池
public class ObjectPool : MonoBehaviour {

	private Queue<GameObject> pool = null;

	void Awake(){
		pool = new Queue<GameObject>();
	}

	public void AddObject(GameObject go)
	{
		go.SetActive(false);
		pool.Enqueue(go);
	}

	public GameObject GetObject()
	{
		GameObject temp= null;
		if(pool.Count > 0)
		{
			temp = pool.Dequeue();
			temp.SetActive(true);
		}
		return temp;
	}

	public bool Data()
	{
		return pool.Count > 0? true: false;
	}
}
