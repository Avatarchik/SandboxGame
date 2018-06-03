using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    private Transform m_Transform;

	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Transform.Rotate(Vector3.up * Random.Range(10, 30));// );
    }
	
	// Update is called once per frame
	void Update () {

	}   
}
