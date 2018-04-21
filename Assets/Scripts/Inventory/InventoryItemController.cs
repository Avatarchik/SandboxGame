using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using System;

/// <summary>
/// 背包内Item物品控制.
/// </summary>
public class InventoryItemController : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler{

    private RectTransform m_RectTransform;
    private CanvasGroup m_CanvasGroup;

    private Text m_Text;    //物品图标
    private Image m_Image;  //物品数量
    private int id;     //自身id
    private bool isDrag = false;    //自身拖拽状态
    public bool inInventory = true;//当前物品是否在背包内，true在背包,false在合成面板
    private int num = 0;    //物品数量.


    private Transform parent;   //物品拖拽过程中父物体
    private Transform self_Parent;//物品自身父物体
    public int Num
    {
        get { return num; }
        set { num = value;
            m_Text.text = num.ToString();//更新UI.
        }
    }
    public int Id { get { return id; } set { id = value; } }
    public bool InInventory
    {
        get { return inInventory; }
        set
        {
            inInventory = value;
            //重置到背包的位置
            m_RectTransform.localPosition = Vector3.zero;
            ResetSpriteSize(m_RectTransform, 85, 85);   
        }
    }

    void Awake()
    {
        FindInit();
    }

    void Update()
    {
        //物品拖拽状态中，按下鼠标右键拆分.
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            BreakMaterials();
        }
    }


    //查找相关的初始化
    private void FindInit()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();

        m_Image = gameObject.GetComponent<Image>();
        m_Text = m_RectTransform.Find("Num").GetComponent<Text>();

        gameObject.name = "InventoryItem";
        parent = GameObject.Find("InventoryPanel").GetComponent<Transform>();
    }


    /// <summary>
    /// 传递数据，初始化Item.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public void InitItem(int id, string name, int num)
    {
        this.id = id;
        m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
        this.num = num;
        m_Text.text = num.ToString();
    }

    /// <summary>
    /// 调用接口实现拖拽.
    /// </summary>
    /// <param name="eventData"></param>
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("开始拖拽");
        self_Parent = m_RectTransform.parent;
        //throw new NotImplementedException();
        m_RectTransform.SetParent(parent);
        //关闭检查射线
        m_CanvasGroup.blocksRaycasts = false;
        isDrag = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Debug.Log("拖拽进行中...");
        //throw new NotImplementedException();
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("结束拖拽");
        //throw new NotImplementedException();
        GameObject target = eventData.pointerEnter;
        //！debug到的一定是自身，因为射线向鼠标位置发射，碰撞到的是自身。

        //拖拽逻辑处理.
        ItemDrag(target);



        //通用重置代码
        //开启检查射线，以免只能拖拽一次
        m_CanvasGroup.blocksRaycasts = true;
        //把拖拽出来的物体层级关系改回去.
        //m_RectTransform.SetParent(target.transform);
        //重置位置(注意不要用position)
        m_RectTransform.localPosition = Vector3.zero;
        isDrag = false;
    }

    /// <summary>
    /// 重置图片尺寸.
    /// </summary>
    private void ResetSpriteSize(RectTransform rectTransform, float width, float heignt)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heignt);
    }

    /// <summary>
    /// 合成材料拆分.
    /// </summary>
    private void BreakMaterials()
    {
        //将当前拖拽中的物品A复制一份->B
        GameObject tempB = GameObject.Instantiate<GameObject>(gameObject);
        RectTransform tempTransform = tempB.GetComponent<RectTransform>();
        //重置B的相关属性：位置、缩放.
        tempTransform.SetParent(self_Parent);
        tempTransform.localPosition = Vector3.zero;
        tempTransform.localScale = Vector3.one;
        //数量拆分.
        int tempCount = num;    //总数量
        int tempNumB = tempCount / 2;
        int tempNumA = tempCount - tempNumB;
        //数量更新.
        tempB.GetComponent<InventoryItemController>().Num = tempNumB;
        Num = tempNumA;
        //恢复射线检测,防止拆分后无法继续拖拽.
        tempB.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //物品id值初始化.(注意克隆时成员生成顺序，必须有这步）
        tempB.GetComponent<InventoryItemController>().Id = Id;
        /**保持原属性不变，背包内可以换位置，合成面板不可以***/
        //tempB.GetComponent<InventoryItemController>().InInventory = InInventory;
    }

    /// <summary>
    /// 合成材料合并.
    /// </summary>
    private void MergeMaterials(InventoryItemController target)
    {
        target.Num += Num;  //数量合并.
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 物品拖拽逻辑.
    /// </summary>
    private void ItemDrag(GameObject target)
    {
        //正常拖拽
        if (target != null)
        {
            //Debug.Log(target.name);
            #region 空的物品槽&&非物品槽
            //拖拽到了空的物品槽位置.
            if (target.tag == "InventorySlot")
            {
                //把拖拽出来的物体层级关系改回去.
                m_RectTransform.SetParent(target.transform);
                ResetSpriteSize(m_RectTransform, 85, 85);

                inInventory = true;
            }
            //拖拽到了非物品槽位置.
            if (target.tag != "InventorySlot")
            {
                m_RectTransform.SetParent(self_Parent);
            }

            #endregion

            /*
            //把拖拽出来的物体层级关系改回去.
            m_RectTransform.SetParent(target.transform);
            //重置位置(注意不要用position)
            m_RectTransform.localPosition = Vector3.zero;*/

            #region 位置物品交换
            //位置物品交换
            if (target.tag == "InventoryItem")
            {
                InventoryItemController iic = target.GetComponent<InventoryItemController>();
                //背包内可以交换.
                if (inInventory && iic.InInventory)
                {
                    if (Id == iic.Id)
                    {
                        //同Id值物品，合并
                        MergeMaterials(iic);
                    }


                    else
                    {
                        //不同Id值，交互位置.
                        Transform tempTarget = target.GetComponent<Transform>();
                        m_RectTransform.SetParent(tempTarget.parent);
                        tempTarget.SetParent(self_Parent);
                        tempTarget.localPosition = Vector3.zero;
                    }

                }
                else
                {
                    //防止合成面板内物品合并.
                    if (Id == iic.Id && iic.InInventory)
                    {
                        //同Id值物品，合并
                        MergeMaterials(iic);
                    }
                }

            }
            #endregion

            #region 拖拽物品到图谱槽.
            //拖拽物品到图谱槽.
            if (target.tag == "CraftingSlot")
            {
                //当前物品槽可以接收物品.
                if (target.GetComponent<CraftingSlotController>().IsOpen)
                {
                    //判断图谱槽与拖拽的物品id是否相同.
                    if (id == target.GetComponent<CraftingSlotController>().Id)
                    {
                        m_RectTransform.SetParent(target.transform);
                        ResetSpriteSize(m_RectTransform, 70, 62);
                        inInventory = false;
                        /***********/
                        //target.GetComponent<CraftingSlotController>().Id = false;
                        //通知合成面板C层. 下面降低下耦合.
                        //CraftingPanelController.Instance.DragMaterialsItem(gameObject);
                        InventoryPanelController.Instance.SendDragMaterialsItem(gameObject);
                    }
                    else
                    {
                        m_RectTransform.SetParent(self_Parent);
                    }
                }
                else//当前物品槽不能接收物品.
                {
                    m_RectTransform.SetParent(self_Parent);
                }
            }
            #endregion
        }
        else//拖拽到非UI位置.
        {
            m_RectTransform.SetParent(self_Parent);
            //m_RectTransform.localPosition = Vector3.zero;
        }
    }
}
