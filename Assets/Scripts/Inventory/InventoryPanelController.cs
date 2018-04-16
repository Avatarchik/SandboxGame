using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///背包模块控制器.
/// </summary>
public class InventoryPanelController : MonoBehaviour {

    //持有VM对象.
    private InventoryPanelModel m_InventoryPanelModel;
    private InventoryPanelView m_InventoryPanelView;

    private int slotNum = 27;

    private List<GameObject> slotList = new List<GameObject>();

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
            slotList.Add(tempSlot);
        }
    }

    /// <summary>
    /// 
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
            temp.GetComponent<InventoryItemController>().InitItem(tempList[i].ItemName, tempList[i].ItemNum);
        }
    }

    
    // Update is called once per frame
    void Update () {
		
	}
}
