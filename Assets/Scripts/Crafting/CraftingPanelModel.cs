using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 合成模块M.
/// </summary>
public class CraftingPanelModel : MonoBehaviour
{

    void Awake()
    {

    }

    /// <summary>
    /// 获取选项卡图标名称.
    /// </summary>
    /// <returns></returns>
    public string[] GetTabsIconName()
    {
        string[] names = new string[] { "Icon_House", "Icon_Weapon" };
        return names;
    }

    /// <summary>
    /// 通过Json文件名实现数据加载.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<List<string>> ByNameGetJsonData(string name)
    {
        List<List<string>> temp = new List<List<string>>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            List<string> tempList = new List<string>();

            JsonData jd = jsonData[i]["Type"];
            for (int j = 0; j < jd.Count; j++)
            {
                
                tempList.Add(jd[j]["ItemName"].ToString());
            }
            temp.Add(tempList);
        }

        return temp;
    }
}
