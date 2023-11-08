using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Unit Azhai;
    public EnemyConfig enemyConfig;
    public Enemy() { }
    public Enemy(GameObject unitObj,Unit Azhai,EnemyConfig enemyConfig) : base(unitObj)
    {
        this.unitObj = unitObj;
        this.Azhai = Azhai;
        this.enemyConfig = enemyConfig;
        isRight = true;
        currentHp = enemyConfig.initHp;
        Init();
    }

    public bool IsAzhaiInAttackRange()
    {
        return GetDistanceBetweenAzhai() <= enemyConfig.attackRange;
    }

    public float GetDistanceBetweenAzhai()
    {
        return Vector3.Distance(unitObj.transform.position, Azhai.unitObj.transform.position);
    }

    public Vector2 moveDirection()
    {
        return new Vector2(Azhai.unitObj.transform.position.x - unitObj.transform.position.x , Azhai.unitObj.transform.position.y-  unitObj.transform.position.y ).normalized;
    }

}
