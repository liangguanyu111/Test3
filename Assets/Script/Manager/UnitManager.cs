using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class UnitManager
{
    public Hero Azhai;
    public List<Enemy> currentEnemy = new List<Enemy>();
    public List<Unit> allUnits = new List<Unit>();

    public void Init()
    {
        CreateAzhai();
        CreatEnemy();
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
        GameObject AzhaiObj = GameObject.Instantiate(Resources.Load<GameObject>("Azhai"),UIManager._instance.unitTransform);
        Azhai = new Hero(AzhaiObj);
        allUnits.Add(Azhai);
    }
    public void CreatEnemy()
    {
        GameObject enenmyObj = GameObject.Instantiate(Resources.Load<GameObject>("Enemy"), UIManager._instance.unitTransform);
        Enemy newEnemy = new Enemy(enenmyObj,Azhai);
        allUnits.Add(newEnemy);
    }
}
