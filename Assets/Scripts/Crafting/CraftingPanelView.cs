using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成模块V.
/// </summary>
public class CraftingPanelView : MonoBehaviour
{

    private Transform m_Transform;
    private Transform tabs_Transform;
    private Transform contents_Transform;
    private Transform center_Transform;

    private GameObject prefab_TabsItem;
    private GameObject prefab_Content;
    private GameObject prefab_ContentItem;
    private GameObject prefab_Slot;
    private GameObject prefab_InventoryItem;

    private Dictionary<string, Sprite> tabIconDic;
    private Dictionary<string, Sprite> materialIconDic;

    public Transform M_Transform { get { return m_Transform; } }
    public Transform Tabs_Transform { get { return tabs_Transform; } }
    public Transform Contents_Transform { get { return contents_Transform; } }
    public Transform Center_Transform { get { return center_Transform; } }
    public GameObject Prefab_TabsItem { get { return prefab_TabsItem; } }

    public GameObject Prefab_Content { get { return prefab_Content; } }
    public GameObject Prefab_ContentItem { get { return prefab_ContentItem; } }

    public GameObject Prefab_Slot { get { return prefab_Slot; } }

    public GameObject Prefab_InventoryItem{get{ return prefab_InventoryItem; }}
    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        tabs_Transform = m_Transform.Find("Left/Tabs").GetComponent<Transform>();
        contents_Transform = m_Transform.Find("Left/Contents").GetComponent<Transform>();
        center_Transform = m_Transform.Find("Center").GetComponent<Transform>();

        prefab_TabsItem = Resources.Load<GameObject>("CraftingTabsItem");
        prefab_Content = Resources.Load<GameObject>("CraftingContent");
        prefab_ContentItem = Resources.Load<GameObject>("CraftingContentItem");
        prefab_Slot = Resources.Load<GameObject>("CraftingSlot");
        prefab_InventoryItem = Resources.Load<GameObject>("InventoryItem");

        tabIconDic = new Dictionary<string, Sprite>();
        materialIconDic = new Dictionary<string, Sprite>();

        //TabsIconLoad();
        //加载所有选项卡图标
        tabIconDic = ResourcesTools.LoadFolderAssets("TabIcon", tabIconDic);
        //合成图谱材料加载
        materialIconDic = ResourcesTools.LoadFolderAssets("Material", materialIconDic);
        //MaterialIconLoad();
    }

    /// <summary>
    /// 加载所有选项卡图标.
    /// </summary>/*
    /*private void TabsIconLoad()
    {
        tabIconDic = ResourcesTools.LoadFolderAssets("TabIcon",tabIconDic);
    }*/

 

    /// <summary>
    /// 合成图谱材料预加载.
    /// </summary>
    /*private void MaterialIconLoad()
    {
        Sprite[] tempSprite = Resources.LoadAll<Sprite>("Material");
        for (int i = 0; i < tempSprite.Length; i++)
        {
            materialIconDic.Add(tempSprite[i].name, tempSprite[i]);
        }
    }*/

    /// <summary>
    /// 通过名称查找字典中的一个图片对象.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite ByNameGetSprite(string name)
    {
        /*
        Sprite temp = null;
        tabIconDic.TryGetValue(name, out temp);
        return temp;*/
        return ResourcesTools.GetAsset(name, tabIconDic);
    }

    /// <summary>
    /// 通过名字获取合成材料的图标资源
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite ByNameGetMaterialIconSprite(string name)
    {
        //Sprite temp = null;
        //materialIconDic.TryGetValue(name, out temp);
        //return temp;
        return ResourcesTools.GetAsset(name, materialIconDic);
    }
}
