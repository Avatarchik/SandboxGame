using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingContentController : MonoBehaviour
{
    private Transform m_Transform;
    private int index = -1;

    private CraftingContentItemController current = null;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    public void InitContent(int index, GameObject prefab, List<CraftingContentItem> strList)
    {
        this.index = index;
        gameObject.name = "Content" + index;
        CreateAllItems(prefab, strList);
    }

    private void CreateAllItems(GameObject prefab, List<CraftingContentItem> strList)
    {
        for (int i = 0; i < strList.Count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab, m_Transform);
            //Debug.Log(strList[i]);
            go.GetComponent<CraftingContentItemController>().Init(strList[i]);
        }
    }

    /// <summary>
    /// 正文区域标题元素状态切换.
    /// </summary>
    /// <param name="item"></param>
    private void ResetItemState(CraftingContentItemController item)
    {
        if (item == current) return;
        Debug.Log(item.Id);
        if (current != null)
        {
            current.NormalItem();
        }
        item.ActiveItem();
        current = item;
        SendMessageUpwards("CreateSlotContents", item.Id);
    }

}
