using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成模块C.
/// </summary>
public class CraftingPanelController : MonoBehaviour
{
    public static CraftingPanelController Instance;

    private Transform m_Transform;

    private CraftingPanelView m_CraftingPanelView;
    private CraftingPanelModel m_CraftingPanelModel;
    private CraftingController m_CraftingController;

    private int tabsNum = 2;
    private List<GameObject> tabsList;
    private List<GameObject> contentsList;

    private int slotsNum = 25;
    private List<GameObject> slotsList;

    private int currentIndex = -1;//防止重复点击

    private int materialsCount = 0; //物品合成需要的材料数，来源于Json
    private int dragMaterialsCount = 0; //合成图品槽存在的材料数，来源于拖拽
    private List<GameObject> materialsList; //存放拖拽放入的材料

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Init();

        CreateAllTabs();
        CreateAllContents();
        ResetTabsAndContents(0);

        CreateAllSlots();
        //CreateSlotContents();

    }


    /// <summary>
    /// 初始化.
    /// </summary>
    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
        m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
        m_CraftingController = m_Transform.Find("Right").GetComponent<CraftingController>();
        tabsList = new List<GameObject>();
        contentsList = new List<GameObject>();
        slotsList = new List<GameObject>();
        materialsList = new List<GameObject>();

        m_CraftingController.Prefab_InventoryItem = m_CraftingPanelView.Prefab_InventoryItem;
    }

    /// <summary>
    /// 生成全部选项卡.
    /// </summary>
    private void CreateAllTabs()
    {
        
        for (int i = 0; i < tabsNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabsItem, m_CraftingPanelView.Tabs_Transform);
            Sprite temp = m_CraftingPanelView.ByNameGetSprite(m_CraftingPanelModel.GetTabsIconName()[i]);
            go.GetComponent<CraftingTabItemController>().InitItem(i, temp);
            tabsList.Add(go);
        }
    }

    private void CreateAllContents()
    {
        List<List<CraftingContentItem>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");
        for (int i = 0; i < tabsNum; i++)
        {

            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content, m_CraftingPanelView.Contents_Transform);
            Sprite temp = m_CraftingPanelView.ByNameGetSprite(m_CraftingPanelModel.GetTabsIconName()[i]);
            go.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem,tempList[i]);
            contentsList.Add(go);

        }
    }

    /// <summary>
    /// 重置选项卡和正文区域.
    /// </summary>
    /// <param name="index"></param>
    private void ResetTabsAndContents(int index)
    {
        if (currentIndex == index) return;

        Debug.Log("Tab " + index);
        for (int i = 0; i < tabsList.Count; i++)
        {
            tabsList[i].GetComponent<CraftingTabItemController>().NormalTab();
            contentsList[i].SetActive(false);
            //slotsList = new List<GameObject>();
        }

        tabsList[index].GetComponent<CraftingTabItemController>().ActiveTab();
        contentsList[index].SetActive(true);
        currentIndex = index;
    }

    /// <summary>
    /// 生成所有的合成图谱槽.
    /// </summary>
    private void CreateAllSlots()
    {
        for (int i = 0; i < slotsNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Slot, m_CraftingPanelView.Center_Transform);
            go.name = "Slot" + i;
            slotsList.Add(go);
        }
        
    }

    /// <summary>
    /// 图谱槽数据填充
    /// </summary>
    private void CreateSlotContents(int id)
    {
        //Debug.Log("SEND: " + id);
        //临时测试
        CraftingMapItem temp = m_CraftingPanelModel.GetItemById(id);
        if (temp != null)
        {
            //清空上一次的图谱
            ResetSlotContents();
            //把图谱内的材料重新放回背包内.
            ResetMaterials();

            //填充
            for (int j = 0; j < temp.MapContents.Length; j++)
            {
                if (temp.MapContents[j] != "0")
                {

                    Sprite sp = m_CraftingPanelView.ByNameGetMaterialIconSprite(temp.MapContents[j]);
                    slotsList[j].GetComponent<CraftingSlotController>().Init(sp,temp.MapContents[j]);
                }
            }
            //最终合成物品图标显示.
            m_CraftingController.Init(temp.MapId, temp.MapName);
            //记录需要的材料数量.
            materialsCount = temp.MaterialsCount;
        }
        /*for (int i = 0; i < tempList.Count; i++)
        {
            Debug.Log(tempList[i].ToString());
        }*/
        
    }

    /// <summary>
    /// 重置图谱.
    /// </summary>
    private void ResetSlotContents()
    {
        for (int i = 0; i < slotsList.Count; i++)
        {
            slotsList[i].GetComponent<CraftingSlotController>().Reset();
        }
    }

    /// <summary>
    /// 重置合成图谱内的材料.(更换合成标签)
    /// </summary>
    private void ResetMaterials()
    {
        List<GameObject> materialsList = new List<GameObject>();
        for (int i = 0; i < slotsList.Count; i++)
        {
            Transform tempTransform = slotsList[i].transform.Find("InventoryItem");
            //有材料就加进去.
            if (tempTransform != null)
            {
                materialsList.Add(tempTransform.gameObject);
            }
        }
        //Debug.Log(materialsLisst.Count);
        InventoryPanelController.Instance.AddItems(materialsList);//单例 传递给背包 由背包处理
    }


    /// <summary>
    /// 对拖入放入合成图谱内的材料进行管理.
    /// </summary>
    /// <param name="item"></param>
    public void DragMaterialsItem(GameObject item)
    {
        materialsList.Add(item);
        dragMaterialsCount++;
        Debug.Log("当前物品合成所需材料数：" + materialsCount + "拖拽放入的材料数：" + dragMaterialsCount);
        //激活合成按钮.
        if (materialsCount == dragMaterialsCount)
        {
            m_CraftingController.ActiveButton();
        }

    }

    /// <summary>
    /// 合成完毕---->材料消耗.
    /// </summary>
    private void CraftingOK()
    {
        for (int i = 0; i < materialsList.Count; i++)
        {
            InventoryItemController iic = materialsList[i].GetComponent<InventoryItemController>();
            if (iic.Num == 1)
            {
                GameObject.Destroy(materialsList[i]);
            }
            else
            {
                iic.Num--;
            }
        }
        StartCoroutine("ResetMap");
    }


    /// <summary>
    /// 合成完成后重置背包.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetMap()
    {
        yield return new WaitForSeconds(2);
        ResetMaterials();
        dragMaterialsCount = 0;
        materialsList.Clear();
    }

}
