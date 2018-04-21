using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private bool inventoryState = false;


    void Start()
    {
        //初始隐藏背包.
        InventoryPanelController.Instance.IUIPanelHide();
    }

    void Update()
    {
        InventoryPanelKey();
        ToolBarPanelKey();

    }

    /// <summary>
    /// 背包按键检测.
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(GameConst.InventoryPAnelKey))
        {
            if (inventoryState)
            {
                inventoryState = false;
                //Debug.Log("隐藏背包");
                InventoryPanelController.Instance.IUIPanelHide();
            }
            else
            {
                inventoryState = true;
                //Debug.Log("显示背包");
                InventoryPanelController.Instance.IUIPanelShow();
            }


        }
    }

    /// <summary>
    /// 工具栏按键检测.
    /// </summary>
    public void ToolBarPanelKey()
    {
        ToolBarKey(GameConst.ToolBarPenleKey_1,0);//后面的参数做索引
        ToolBarKey(GameConst.ToolBarPenleKey_2,1);
        ToolBarKey(GameConst.ToolBarPenleKey_3,2);
        ToolBarKey(GameConst.ToolBarPenleKey_4,3);
        ToolBarKey(GameConst.ToolBarPenleKey_5,4);
        ToolBarKey(GameConst.ToolBarPenleKey_6,5);
        ToolBarKey(GameConst.ToolBarPenleKey_7,6);
        ToolBarKey(GameConst.ToolBarPenleKey_8,7);
    }

    /// <summary>
    /// 工具栏--单个按键检测.
    /// </summary>
    /// <param name="keycode"></param>
    private void ToolBarKey(KeyCode keycode,int keyNum)
    {
        if (Input.GetKeyDown(keycode))
        {
            ToolBarPanelController.Instance.SaveActiveSlotByKeyCode(keyNum);
        }
    }
}
