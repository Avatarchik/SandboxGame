using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTabItemController : MonoBehaviour
{

    private Transform m_Transform;
    private Button m_Button;
    private GameObject m_ButtonBG;
    private Image m_Icon;

    private int index = -1;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Button = gameObject.GetComponent<Button>();
        m_ButtonBG = m_Transform.Find("Center_BG").gameObject;
        m_Icon = m_Transform.Find("Icon").GetComponent<Image>();
        m_Button.onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
    /// 初始化Item.
    /// </summary>
    public void InitItem(int index, Sprite icon)
    {
        this.index = index;
        gameObject.name = "Tab" + index;
        m_Icon.sprite = icon;
    }

    /// <summary>
    /// 默认选项卡状态.
    /// </summary>
    public void NormalTab()
    {
        if (m_ButtonBG.activeSelf == false)
        {
            m_ButtonBG.SetActive(true);
        }
    }

    /// <summary>
    /// 激活选项卡状态.
    /// </summary>
    public void ActiveTab()
    {
        m_ButtonBG.SetActive(false);
    }

    private void ButtonOnClick()
    {
        SendMessageUpwards("ResetTabsAndContents", index);
    }
}
