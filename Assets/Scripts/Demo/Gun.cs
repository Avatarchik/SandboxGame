using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase, IGun
{
    public void Shoot()
    {
        //throw new System.NotImplementedException();
        Debug.Log("接口瞄准");
    }

    public void ShootReady()
    {
        //throw new System.NotImplementedException();
        Debug.Log("接口sheji ");
    }

    /*
public override void Shoot()
{
   Debug.Log("sheji");
}

public override void ShootReady()
{
   Debug.Log("miaozhun");;
}*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ShootReady();
        if (Input.GetKeyDown(KeyCode.B))
            Shoot();


    }
}
