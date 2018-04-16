using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingContentItemController : MonoBehaviour {


    private Transform m_Transform;
    private Text m_Text;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Text = m_Transform.Find("Text").GetComponent<Text>();
    }


    public void Init(string name)
    {
        m_Text.text = "  " + name;
    }

}
