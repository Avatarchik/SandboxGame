using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarSlotController : MonoBehaviour {


    private Transform m_Transform;
    private Button m_Button;
    private Image m_Image;

    private Text m_Text;

    private bool selfState = false; //false为默认状态，true为激活状态

    public bool SelfState
    {
        get { return selfState; }
    }

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Button = gameObject.GetComponent<Button>();
        m_Image = gameObject.GetComponent<Image>();

        m_Text = m_Transform.Find("Key").GetComponent<Text>();
        m_Button.onClick.AddListener(SlotClick);
    }

    public void Init(string name, int keyNum)
    {
        gameObject.name = name;
        m_Text.text = keyNum.ToString();
    }

    public void SlotClick()
    {
        Debug.Log("单击");
        if(selfState)
        {
            Normal();

        }
        else
        {
            Active();
        }
        SendMessageUpwards("SaveActiveSlot",gameObject);
    }


    public void Normal()
    {
        m_Image.color = Color.white;
        selfState = false;
    }

    private void Active()
    {
        m_Image.color = Color.red;
        selfState = true;

    }
}
