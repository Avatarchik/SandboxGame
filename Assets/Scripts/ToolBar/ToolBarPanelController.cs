using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarPanelController : MonoBehaviour {

    public static ToolBarPanelController Instance;

    private ToolBarPanelView m_ToolBarPanelView;
    private ToolBarPanelModel m_ToolBarPanelModel;

    private List<GameObject> slotList = null;    //工具栏slot集合
    private GameObject currentActive = null;    //存放当前激活的物品.

    void Awake()
    {
        Instance = this;
    }

    void Start () {
        Init();
        CreateAllSlot();
    }
	
    private void Init()
    {
        m_ToolBarPanelView = gameObject.GetComponent<ToolBarPanelView>();
        m_ToolBarPanelModel = gameObject.GetComponent<ToolBarPanelModel>();
        slotList = new List<GameObject>();
    }

    /// <summary>
    /// 生成所有工具栏物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        
        for (int i = 0; i < 8; i++)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_ToolBarPanelView.Prefab_ToolBarSlot, m_ToolBarPanelView.Grid_Transform);
            //slot.transform.Find("Key").GetComponent<Text>().text = (i + 1).ToString();
            //slot.name = "ToolBarSllot" + i;
            //slot.name = m_ToolBarPanelView.Prefab_ToolBarSlot.name + "_" + i;
            slot.GetComponent<ToolBarSlotController>().Init(m_ToolBarPanelView.Prefab_ToolBarSlot.name + "_" + i, i + 1);
            slotList.Add(slot);
        }
    }

    /// <summary>
    /// 存储当前激活的物品槽以及物品.
    /// </summary>
    /// <param name="activeSlot"></param>
    private void SaveActiveSlot(GameObject activeSlot)
    {
        if((currentActive!=null) && (currentActive!=activeSlot))
        {
            currentActive.GetComponent<ToolBarSlotController>().Normal();
            currentActive = null;
        }
        currentActive = activeSlot;
    }

    public void SaveActiveSlotByKeyCode(int keyNum)
    {
        if ((currentActive != null) && (currentActive != slotList[keyNum]))
        {
            currentActive.GetComponent<ToolBarSlotController>().Normal();
            currentActive = null;
        }

        currentActive = slotList[keyNum];
        currentActive.GetComponent<ToolBarSlotController>().SlotClick();
    }

}





