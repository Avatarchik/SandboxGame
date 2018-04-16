using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包内Item物品控制.
/// </summary>
public class InventoryItemController : MonoBehaviour
{

    private Transform m_Transform;
    private Text m_Text;
    private Image m_Image;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = gameObject.GetComponent<Image>();
        m_Text = m_Transform.Find("Num").GetComponent<Text>();

    }

    /// <summary>
    /// 传递数据，初始化Item.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public void InitItem(string name, int num)
    {
        m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
        m_Text.text = num.ToString();
    }


}
