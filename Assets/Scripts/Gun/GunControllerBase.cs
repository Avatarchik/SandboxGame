using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械C层父类
/// </summary>
public abstract class GunControllerBase : MonoBehaviour {
    //组件
    private GunViewBase m_GunViewBase;
    private Ray ray;
    private RaycastHit hit;

    //数值
    [SerializeField] private int id; //ID
    [SerializeField] private int damage; //伤害
    [SerializeField] private int durable; //耐久
    [SerializeField] private GunType gunWeaponType;

    //特效
    private AudioClip audio; //声音特效
    private GameObject effect; //动画特效

    //标识
    private bool canShoot = true;

    #region 属性
    public GunViewBase M_GunViewBase
    {
        get { return m_GunViewBase; }
        set { m_GunViewBase = value; }
    }
    public Ray Ray
    {
        get
        {
            return Ray;
        }

        set
        {
            Ray = value;
        }
    }
    public RaycastHit Hit
    {
        get
        {
            return hit;
        }

        set
        {
            hit = value;
        }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Durable
    {
        get { return durable; }
        set
        {
            durable = value;
            if (durable <= 0)
            {
                //销毁自身
                GameObject.Destroy(gameObject);
                //销毁准星
                GameObject.Destroy(m_GunViewBase.M_Gunstar.gameObject);
            }
        }
    }
    public GunType GunWeaponType
    {
        get { return gunWeaponType; }
        set { gunWeaponType = value; }
    }
    public AudioClip Audio
    {
        get { return audio; }
        set { audio = value; }
    }
    public GameObject Effect
    {
        get { return effect; }
        set { effect = value; }
    }
    #endregion

    //播放音效
    public void PlayAudio()
    {
        //Debug.Log ("播放音效");
        AudioSource.PlayClipAtPoint(Audio, M_GunViewBase.M_GunPoint.position);
    }

    //射击准备
    public void ShootReady()
    {
        ray = new Ray(M_GunViewBase.M_GunPoint.position, M_GunViewBase.M_GunPoint.forward);
        //Debug.DrawLine(m_AssaultRifleView.M_GunPoint.position, m_AssaultRifleView.M_GunPoint.forward * 500, Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            Vector2 uiPos = RectTransformUtility.WorldToScreenPoint(M_GunViewBase.M_EnvCamera, hit.point);
            Debug.DrawLine(M_GunViewBase.M_GunPoint.position, M_GunViewBase.M_GunPoint.forward * 1000, Color.red);
            M_GunViewBase.M_Gunstar.position = uiPos;
            Debug.Log("碰撞");
        }
        else
        {
            hit.point = Vector3.zero;
            Debug.Log("无碰撞");
        }

    }

    //协程延迟
    public IEnumerator Delay(ObjectPool pool, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);

    }

    //鼠标控制
    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && canShoot) //按下鼠标左键->射击.
        {
            MouseButtonLeftDown();
        }
        if (Input.GetMouseButton(1)) //按住鼠标右键->开镜  
        {
            MouseButtonRightDown();
        }
        if (Input.GetMouseButtonUp(1)) //松开鼠标右键，退出开镜    
        {
            MouseButtonUp();
        }
    }
    private void MouseButtonLeftDown()
    {
        Shoot();
        PlayAudio();
        PlayEffect();
        M_GunViewBase.M_Animator.SetTrigger("Fire");
    }
    private void MouseButtonRightDown()
    {
        M_GunViewBase.M_Animator.SetBool("HoldPose", true);
        M_GunViewBase.EnterHoldPose();
        M_GunViewBase.M_Gunstar.gameObject.SetActive(false);
    }
    private void MouseButtonUp()
    {
        M_GunViewBase.M_Animator.SetBool("HoldPose", false);
        M_GunViewBase.ExitHoldPose();
        M_GunViewBase.M_Gunstar.gameObject.SetActive(true);
    }

    public void CanShoot(int state)
    {
        if (state == 0)
            canShoot = false;
        if (state == 1)
            canShoot = true;
    }

    public abstract void Init();
    public abstract void LoadAudioAsset();
    public abstract void LoadEffectAsset();

    public abstract void Shoot();
    public abstract void PlayEffect();

    public virtual void Start()
    {
        m_GunViewBase = gameObject.GetComponent<GunViewBase>();
        LoadAudioAsset();
        LoadEffectAsset();
        Init();
    }

    public void Update()
    {
        ShootReady();
        MouseControl();
    }

}
