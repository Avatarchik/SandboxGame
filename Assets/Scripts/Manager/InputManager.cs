using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class InputManager : MonoBehaviour {

    private bool inventoryState = false;

    private FirstPersonController m_FirstPersonController;
    //private GunControllerBase m_GunControllerBase;
    //private GameObject m_GunStar;

    void Start()
    {
        //初始隐藏背包.
        InventoryPanelController.Instance.IUIPanelHide();
        FindInit();
    }

    void Update()
    {
        InventoryPanelKey();
        if(inventoryState == false)
            ToolBarPanelKey();

    }

    private void FindInit()
    {
        m_FirstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

    }

    /// <summary>
    /// 背包按键检测.
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(GameConst.InventoryPAnelKey))
        {
            if (inventoryState) //背包关闭.
            {
                inventoryState = false;
                //Debug.Log("隐藏背包");
                InventoryPanelController.Instance.IUIPanelHide();
                m_FirstPersonController.enabled = true;
                //m_GunControllerBase.enabled = true;
                //m_GunStar.SetActive(true);
                if(ToolBarPanelController.Instance.CurrentActiveModel != null)
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(true);
            }
            else //背包打开.
            {
                inventoryState = true;
                //Debug.Log("显示背包");
                InventoryPanelController.Instance.IUIPanelShow();
                m_FirstPersonController.enabled = false;
                //m_GunControllerBase.enabled = false;
                if (ToolBarPanelController.Instance.CurrentActiveModel != null)
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //m_GunStar.SetActive(false);
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
