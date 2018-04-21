using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotController : MonoBehaviour {

    private Transform m_Transform;
    private Image m_Image;
    private CanvasGroup m_CanvasGroup;

    private bool isOpen = false; //表示当前图谱槽不能接收物品. 
    public bool IsOpen { get { return isOpen; } }

    private int id = -1;    //图谱槽物品自身id.
    public int Id { get { return id; } }

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("Item").GetComponent<Image>();
        m_CanvasGroup = m_Transform.Find("Item").GetComponent<CanvasGroup>();
        m_Image.gameObject.SetActive(false);
        
    }

    public void Init(Sprite sprite, string id)
    {
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = sprite;

        //让参考合成材料忽略射线检测.
        m_CanvasGroup.blocksRaycasts = false;
        //m_Image.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
        //当前位置可以接收物品.
        isOpen = true;

        //接收自身的id
        this.id = int.Parse(id);

    }

    public void Reset()
    {
        m_Image.gameObject.SetActive(false);
    }

}
