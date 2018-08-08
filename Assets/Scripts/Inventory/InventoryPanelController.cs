using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///背包模块控制器.
/// </summary>
public class InventoryPanelController : MonoBehaviour,IUIPanelShowHide {

    public static InventoryPanelController Instance;//通过单例传递数据

    //持有VM对象.
    private InventoryPanelModel m_InventoryPanelModel;
    private InventoryPanelView m_InventoryPanelView;

    private int slotNum = 27;

    private List<GameObject> slotList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }



    void Start () {
        m_InventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
        m_InventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();

        CreateAllSlot();
        CreateAllItem();

    }
	
    /// <summary>
    /// 生成全部物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        for(int i = 0; i < slotNum; i++)
        {
            GameObject tempSlot = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Slot, m_InventoryPanelView.GetGridTransform);
            tempSlot.name = "InventorySlot_" + i;
            slotList.Add(tempSlot);
        }
    }

    /// <summary>
    /// 生成全部物品项.
    /// </summary>
    private void CreateAllItem()
    {
        List<InventoryItem> tempList = m_InventoryPanelModel.GetJsonList("InventoryJsonData");

        /*
        List<InventoryItem> tempList = new List<InventoryItem>();
        tempList.Add(new InventoryItem("Torch", 10));
        tempList.Add(new InventoryItem("Axe", 23));
        tempList.Add(new InventoryItem("Arrow", 15));
        */
        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject temp = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Item, slotList[i].GetComponent<Transform>());
            temp.GetComponent<InventoryItemController>().InitItem(tempList[i].ItemId,tempList[i].ItemName, tempList[i].ItemNum, tempList[i].ItemBar);
        }
    }

    
    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 往背包内填充材料.
    /// </summary>
    /// <param name="itemList"></param>
    public void AddItems(List<GameObject> itemList)
    {
        int tempIndex = 0;
        for (int i = 0; i < slotList.Count; i++)
        {
            Transform tempTransform = slotList[i].transform.Find("InventoryItem");
            //==null 说明是一个空的物品槽.
            if (tempTransform == null && tempIndex < itemList.Count)   
            {
                itemList[tempIndex].transform.SetParent(slotList[i].transform);
                itemList[tempIndex].GetComponent<InventoryItemController>().InInventory = true;
                tempIndex++;
            }
        }

    }

    /// <summary>
    /// 两个C层传数据，降低耦合.
    /// </summary>
    /// <param name="item"></param>
    public void SendDragMaterialsItem(GameObject item)
    {
        CraftingPanelController.Instance.DragMaterialsItem(item);
    }

    public void IUIPanelShow()
    {
        gameObject.SetActive(true);
    }

    public void IUIPanelHide()
    {
        gameObject.SetActive(false);
    }
}
