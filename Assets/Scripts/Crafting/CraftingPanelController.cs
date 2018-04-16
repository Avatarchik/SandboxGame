using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成模块C.
/// </summary>
public class CraftingPanelController : MonoBehaviour
{

    private CraftingPanelView m_CraftingPanelView;
    private CraftingPanelModel m_CraftingPanelModel;

    private int tabsNum = 2;
    private List<GameObject> tabsList;
    private List<GameObject> contentsList;

    // Use this for initialization
    void Start()
    {
        Init();

        CreateAllTabs();
        CreateAllContents();
        ResetTabsAndContents(0);
    }


    /// <summary>
    /// 初始化.
    /// </summary>
    private void Init()
    {
        m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
        m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
        tabsList = new List<GameObject>();
        contentsList = new List<GameObject>();
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
        List<List<string>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");
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
        for (int i = 0; i < tabsList.Count; i++)
        {
            tabsList[i].GetComponent<CraftingTabItemController>().NormalTab();
            contentsList[i].SetActive(false);
        }

        tabsList[index].GetComponent<CraftingTabItemController>().ActiveTab();
        contentsList[index].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
