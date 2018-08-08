using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFactory : MonoBehaviour {

    public static GunFactory Instance;
    private Transform m_Transform;

    private GameObject prefab_AssaultRifle;
    private GameObject prefab_Shotgun;
    private GameObject prefab_WoodenBow;
    private GameObject prefab_WoodenSpear;

    private int index = 0;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        PrefabLoad();
    }

    private void PrefabLoad()
    {
        prefab_AssaultRifle = Resources.Load<GameObject>("Gun/Prefabs/Assault Rifle");
        prefab_Shotgun = Resources.Load<GameObject>("Gun/Prefabs/Shotgun");
        prefab_WoodenBow = Resources.Load<GameObject>("Gun/Prefabs/Wooden Bow");
        prefab_WoodenSpear = Resources.Load<GameObject>("Gun/Prefabs/Wooden Spear");
    }

    public GameObject CreateGun(string gunName, GameObject icon)
    {
        GameObject tempGun = null;
        switch (gunName)
        {
            case "Assault Rifle":
                tempGun = GameObject.Instantiate<GameObject>(prefab_AssaultRifle, m_Transform);
                InitGun(tempGun, 100, 15, GunType.AssaultRifle, icon);
                break; 

            case "Shotgun":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Shotgun, m_Transform);
                InitGun(tempGun, 100, 20, GunType.Shotgun, icon);
                break;

            case "Wooden Bow":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenBow, m_Transform);
                InitGun(tempGun, 100, 25, GunType.WoodenBow, icon);
                break;

            case "Wooden Spear":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenSpear, m_Transform);
                InitGun(tempGun, 100, 30, GunType.WoodenSpear, icon);
                break;
        }
        return tempGun;
    }

    private void InitGun(GameObject gun, int damage, int durable, GunType type, GameObject icon)
    {

        GunControllerBase gcb = gun.GetComponent<GunControllerBase>();
        gcb.Id = index++;
        gcb.Damage = damage;
        gcb.Durable = durable;
        gcb.GunWeaponType = type;
        gcb.ToolBarIcon = icon;
    }
}
