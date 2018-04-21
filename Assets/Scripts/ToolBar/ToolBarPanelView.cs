using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanelView : MonoBehaviour {

    private Transform m_Transform;
    private Transform grid_Transform;
    
    private GameObject prefab_ToolBarSlot;

    public Transform M_Transform { get { return m_Transform; } }
    public Transform Grid_Transform { get {  return grid_Transform; } }
    public GameObject Prefab_ToolBarSlot { get { return prefab_ToolBarSlot; } }

    //public GameObject

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        grid_Transform = m_Transform.Find("Grid").GetComponent<Transform>();

        prefab_ToolBarSlot = Resources.Load<GameObject>("ToolBarSlot");
    }
}
