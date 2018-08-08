using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunWeaponBase : GunControllerBase {

    protected override void Start()
    {
        base.Start();
        LoadEffectAsset();
    }

    protected override void MouseButtonLeftDown()
    {
        base.MouseButtonLeftDown();
        PlayEffect();
    }

    protected abstract void LoadEffectAsset();

    protected abstract void PlayEffect();
}
