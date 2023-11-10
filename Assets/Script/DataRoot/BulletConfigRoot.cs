using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConfigRoot 
{
    public Dictionary<int, BulletConfig> bulletConfigDic;
    public BulletConfigRoot()
    {
        bulletConfigDic = new Dictionary<int, BulletConfig>();
        List<BulletConfig> bulletConfigs = new List<BulletConfig>();
        JsaonMgr.LoadFromPath<BulletConfig>("/bulletCfg.txt", out bulletConfigs);
        foreach (var bulletConfig in bulletConfigs)
        {
            bulletConfigDic.Add(bulletConfig.bulletID, bulletConfig);
        }
    }
    public BulletConfig GetBulletCfg(int id)
    {
        if(bulletConfigDic.ContainsKey(id))
        {
            return bulletConfigDic[id];
        }

        return null;
    }
}
