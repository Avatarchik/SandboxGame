using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour {

    private Transform m_Transform;
    private Transform[] points;
    private Transform stone_Transform;

    private GameObject prefab_Stone;
    private GameObject prefab_Stone_1;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        stone_Transform = m_Transform.Find("Stones");
        points = m_Transform.Find("StonePoints").GetComponentsInChildren<Transform>();
        prefab_Stone = Resources.Load<GameObject>("Env/Rock_Normal");
        prefab_Stone_1 = Resources.Load<GameObject>("Env/Rock_Metal");

        for (int i = 1; i < points.Length; i++)
        {
            points[i].GetComponent<MeshRenderer>().enabled = false;
        }

        for (int i = 1; i < points.Length; i++)
        {
            int index = Random.Range(0, 2);
            GameObject prefab;
            if (index == 0)
                prefab = prefab_Stone;
            else
                prefab = prefab_Stone_1;

            Transform stone = GameObject.Instantiate<GameObject>(prefab, points[i].localPosition, Quaternion.identity, stone_Transform).GetComponent<Transform>();
            float size = Random.Range(0.5f, 2.5f);
            stone.localScale = stone.localScale * size;

            Vector3 rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            stone.localRotation = Quaternion.Euler(rot);
        }
    }

}
