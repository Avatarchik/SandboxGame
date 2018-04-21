using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMapItem{

    private int mapId;              //图谱id
    private string[] mapContents;   //图谱正文
    private int materialsCount;     //图谱材料数量
    private string mapName;         //图谱物品

    public int MapId
    {
        get { return mapId; }
        set { mapId = value; }
    }

    public string[] MapContents
    {
        get { return mapContents; }
        set { mapContents = value; }
    }

    public int MaterialsCount
    {
        get { return materialsCount; }
        set { materialsCount = value; }
    }

    public string MapName
    {
        get { return mapName; }
        set { mapName = value; }
    }

    public CraftingMapItem(int mapId, string[] mapContents, int materialsCount,string mapName)
    {
        this.mapId = mapId;
        this.mapContents = mapContents;
        this.materialsCount = materialsCount;
        this.mapName = mapName;
    }

    public override string ToString()
    {
        return string.Format("ID:{0}, map:{1}, name{2},count{3}", this.mapId, this.MapContents.Length, this.mapName, this.materialsCount);
    }


}
