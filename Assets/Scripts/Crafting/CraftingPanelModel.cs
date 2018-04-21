using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 合成模块M.
/// </summary>
public class CraftingPanelModel : MonoBehaviour
{
    private Dictionary<int, CraftingMapItem> mapItemDic = null;
    void Awake()
    {
        mapItemDic = LoadMapContents("CraftingMapJsonData");
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
    public List<List<CraftingContentItem>> ByNameGetJsonData(string name)
    {
        List<List<CraftingContentItem>> temp = new List<List<CraftingContentItem>>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            List<CraftingContentItem> tempList = new List<CraftingContentItem>();

            JsonData jd = jsonData[i]["Type"];
            for (int j = 0; j < jd.Count; j++)
            {
                tempList.Add(JsonMapper.ToObject<CraftingContentItem>(jd[j].ToJson()));
                //tempList.Add(jd[j]["ItemName"].ToString()); 数据实体类-重构
            }
            temp.Add(tempList);
        }

        return temp;
    }

    /// <summary>
    /// 加载合成图谱Json数据.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private Dictionary<int, CraftingMapItem> LoadMapContents(string name)
    {
        Dictionary<int, CraftingMapItem> temp = new Dictionary<int, CraftingMapItem>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            //取临时数据
            int mapId = int.Parse(jsonData[i]["MapId"].ToString());
            string tempStr = jsonData[i]["MapContents"].ToString();
            string[] mapContents = tempStr.Split(',');
            int mapCount = int.Parse(jsonData[i]["MaterialsCount"].ToString()); ;
            string mapName = jsonData[i]["MapName"].ToString();
            //构造对象
            CraftingMapItem item = new CraftingMapItem(mapId, mapContents, mapCount, mapName);
            temp.Add(mapId, item);

        }


        return temp;
    }

    /// <summary>
    /// 通过ID获取对应的合成图谱
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CraftingMapItem GetItemById(int id)
    {
        CraftingMapItem temp = null;
        mapItemDic.TryGetValue(id, out temp);
        return temp;
    }




}
