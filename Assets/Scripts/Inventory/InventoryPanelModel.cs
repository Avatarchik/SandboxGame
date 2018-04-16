using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 背包数据控制.
/// </summary>
public class InventoryPanelModel : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

    }

    /// <summary>
    /// 通过Json文件名获取List对象
    /// </summary>
    /// <param name="fileName">Json文件名</param>
    /// <returns>List对象</returns>
    public List<InventoryItem> GetJsonList(string fileName)
    {
        List<InventoryItem> tempList = new List<InventoryItem>();
        string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;

        //解析Json
        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            InventoryItem ii = JsonMapper.ToObject<InventoryItem>(jsonData[i].ToJson());
            tempList.Add(ii);
        }
        return tempList;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
