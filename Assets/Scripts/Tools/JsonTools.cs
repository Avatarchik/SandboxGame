using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 通过文件名称加载Json文件.
/// </summary>
public sealed class JsonTools{

    public static List<T> LoadJsonFile<T>(string fileName)
    {
        List<T> tempList = new List<T>();
        string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;

        //解析Json
        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }
        return tempList;
    }

}
