using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据实体.
/// </summary>
public class CraftingContentItem : MonoBehaviour {

    private int itemId;
    private string itemName;

    public int ItemID
    {
        get { return itemId; }
        set { itemId = value; }

    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }

    }




}
