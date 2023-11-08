using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitConfigRoot
{
    public Dictionary<int, EnemyConfig> enemyCfgDic;
    public HeroConfig heroCfg;
    public UnitConfigRoot()
    {
        enemyCfgDic = new Dictionary<int, EnemyConfig>();
        List<EnemyConfig> enemyConfigs = new List<EnemyConfig>();
        List<HeroConfig> heroConfig = new List<HeroConfig>();
        if (JsaonMgr.LoadFromPath<EnemyConfig>("/enemyCfg.txt", out enemyConfigs))
        {
            foreach (var enemyConfig in enemyConfigs)
            {
                enemyCfgDic.Add(enemyConfig.enemyID, enemyConfig);
            }
        }
        if(JsaonMgr.LoadFromPath<HeroConfig>("/heroCfg.txt", out heroConfig))
        {
            heroCfg = heroConfig[0];
        }
    }

    public EnemyConfig GetEnemyCfgByID(int id)
    {
        if (enemyCfgDic.ContainsKey(id))
        {
            return enemyCfgDic[id];
        }
        
        return new EnemyConfig();
    }
}
