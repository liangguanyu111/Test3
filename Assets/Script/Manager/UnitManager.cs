using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[SerializeField]
public class UnitManager
{
    public Hero Azhai;
    public Transform unitTransform;
    public List<Unit> allUnits = new List<Unit>();

    public UnitConfigRoot unitConfigRoot;
    public void Init()
    {
        unitConfigRoot = new UnitConfigRoot();
        unitTransform = GameObject.Find("Units").transform;
        CreateAzhai();
    }
    public void Update()
    {
        //倒序遍历避免增添问题
        for (int i = allUnits.Count-1; i>=0 ; i--)
        {
            allUnits[i].Update();
        }
    }
    public void CreateAzhai()
    {   
        GameObject AzhaiObj = GameObject.Instantiate(Resources.Load<GameObject>("Azhai"), unitTransform);
        Azhai = new Hero(AzhaiObj,unitConfigRoot.heroCfg);
        allUnits.Add(Azhai);
    }


    public Enemy CreateEnemy(int id)
    {
        EnemyConfig enemyCfg = unitConfigRoot.GetEnemyCfgByID(id);
        GameObject enenmyObj = GameObject.Instantiate(Resources.Load<GameObject>(enemyCfg.enemyObjName), GameManager._instance.room.ReturnRandomPosInRoom(), Quaternion.identity, unitTransform);
        Enemy newEnemy = new Enemy();
        switch (enemyCfg.enemyObjName)
        {
            case "EnemyMelee":
                newEnemy = new EnemyMelee(enenmyObj, Azhai, enemyCfg);
                break;
            case "EnemyRange":
                newEnemy = new EnemyRange(enenmyObj, Azhai, enemyCfg);
                break;
        }

        allUnits.Add(newEnemy);
        newEnemy.OnUnitDead += RemoveEnemy;
        return newEnemy;
    }

    public void RemoveEnemy(Unit enemy)
    {
        enemy.unitObj.SetActive(false);
        allUnits.Remove(enemy);
    }
}
