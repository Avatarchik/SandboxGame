﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品的数据实体类.
/// </summary>
public class InventoryItem
{
    private int itemId;
    private string itemName;
    private int itemNum;

    public int ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    public string ItemName
    {
        get{ return itemName; }
        set{ itemName = value; }
    }

    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }

    public InventoryItem() { }
    public InventoryItem(int itemId,string itemName, int itemNum)
    {
        this.ItemId = itemId;
        this.ItemName = itemName;
        this.ItemNum = itemNum;
    }

    public override string ToString()
    {
        return string.Format("物品名称：{0}，数量：{1}, Id：{2}", this.itemName, this.itemNum,this.itemId);
    }


}
