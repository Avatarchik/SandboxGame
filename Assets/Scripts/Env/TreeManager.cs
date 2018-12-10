using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour {

    private Transform m_Transform;
    private Transform tree_Transform;
    private Transform[] points;

    private GameObject prefab_Tree;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        tree_Transform = m_Transform.Find("Trees");
        points = m_Transform.Find("TreePoints").GetComponentsInChildren<Transform>();
         
        prefab_Tree = Resources.Load<GameObject>("Env/Conifer");

        for (int i = 1; i < points.Length; i++)
        {
            points[i].GetComponent<MeshRenderer>().enabled = false;
        }

        for(int i = 1; i < points.Length; i++)
        {
            Transform tree = GameObject.Instantiate<GameObject>(prefab_Tree, points[i].localPosition, Quaternion.identity, tree_Transform).GetComponent<Transform>();

            float height = Random.Range(0.5f, 1.0f);
            tree.localScale *= height;
            //test
            tree.gameObject.name = "Conifer";
        }
	}
	
}
