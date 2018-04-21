using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源工具类
/// </summary>
public sealed class ResourcesTools{

	
    /// <summary>
    /// 加载文件夹资源.
    /// </summary>
    public static Dictionary<string, Sprite> LoadFolderAssets(string folderName, Dictionary<string,Sprite> dic)
    {
        Sprite[] temSprite = Resources.LoadAll<Sprite>(folderName);
        for (int i = 0; i < temSprite.Length; i++)
        {
            dic.Add(temSprite[i].name, temSprite[i]);
        }
        return dic;
    }


    /// <summary>
    /// 通过名字获取资源.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Sprite GetAsset(string fileName, Dictionary<string, Sprite> dic)
    {
        Sprite temp = null;
        dic.TryGetValue(fileName, out temp);
        return temp;
    }

   

}
