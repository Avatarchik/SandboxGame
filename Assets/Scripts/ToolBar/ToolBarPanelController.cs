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
    private GameObject currentActiveModel = null; //存储当前激活的角色模型.
    private int currentKeyCode = -1;

    private Dictionary<GameObject, GameObject> toolBarDic = null;

    public GameObject CurrentActiveModel
    {
        get { return currentActiveModel; }
    }

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
        toolBarDic = new Dictionary<GameObject, GameObject>();
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
        if (slotList[keyNum].GetComponent<Transform>().Find("InventoryItem") == null)
        {
            return;
        }
        if ((currentActive != null) && (currentActive != slotList[keyNum]))
        {
            currentActive.GetComponent<ToolBarSlotController>().Normal();
            currentActive = null;
        }

        currentActive = slotList[keyNum];
        currentActive.GetComponent<ToolBarSlotController>().SlotClick();

        if (currentKeyCode == keyNum && currentActiveModel != null)
        {
            //武器卸下
            currentActiveModel.SetActive(false);
            currentActiveModel = null;
        }
        else
        {
            //武器切换
            FindInventoryItem();
        }

        //存储用户的按键
        currentKeyCode = keyNum;
    }

    //调用枪械工厂类
    private void FindInventoryItem()
    {
        Transform m_temp = currentActive.GetComponent<Transform>().Find("InventoryItem");
        StartCoroutine("CallGunFactory", m_temp);
    }

    private IEnumerator CallGunFactory(Transform m_temp)
    {
        if (m_temp != null)
        {
            //隐藏当前显示的角色模型
            if (currentActiveModel != null)
            {
                currentActiveModel.GetComponent<GunControllerBase>().Holster();
                yield return new WaitForSeconds(0.6f);
                currentActiveModel.SetActive(false);
            }

            //判断字典里是否存在
            GameObject temp = null;
            toolBarDic.TryGetValue(m_temp.gameObject, out temp);
            //字典中未找到
            if (temp == null)
            {
                //调用枪械武器工厂，创建一个新的武器
                temp = GunFactory.Instance.CreateGun(m_temp.GetComponent<Image>().sprite.name, m_temp.gameObject);
                //新数据加入字典
                toolBarDic.Add(m_temp.gameObject, temp);
            }
            else
            {
                if (currentActive.GetComponent<ToolBarSlotController>().SelfState)
                    temp.SetActive(true);
            }
            currentActiveModel = temp;
        }
    }
}





