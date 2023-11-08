using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[SerializeField]
public class UnitManager
{
    public Hero Azhai;
    public Transform unitTransform;
    public UnitConfigRoot unitConfigRoot;

    public List<Unit> allUnits = new List<Unit>();
    //怪物对象池，一个ID一个对象池
    public Dictionary<int, ObjectPool<Enemy>> enemyObjectPool;

    public void Init()
    {
        unitConfigRoot = new UnitConfigRoot();
        unitTransform = GameObject.Find("Units").transform;
        enemyObjectPool = new Dictionary<int, ObjectPool<Enemy>>();
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

    public Enemy GetEnemy(int id)
    {
        if(!enemyObjectPool.ContainsKey(id))
        {
            enemyObjectPool.Add(id, new ObjectPool<Enemy>(()=>{return CreateEnemy(id);},ResetEnemy));  
        }
        return enemyObjectPool[id].New();
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
        enemy.OnUnitDead -= RemoveEnemy;
        allUnits.Remove(enemy);
        enemy.unitObj.SetActive(false);
        enemyObjectPool[(enemy as Enemy).enemyConfig.enemyID].ReturnObj(enemy as Enemy);
    }
    public void ResetEnemy(Enemy enemy)
    {
        enemy.Reset();
        enemy.OnUnitDead += RemoveEnemy;
        enemy.unitObj.transform.position = GameManager._instance.room.ReturnRandomPosInRoom();       
        enemy.unitObj.SetActive(true);
        allUnits.Add(enemy);
    }
}
