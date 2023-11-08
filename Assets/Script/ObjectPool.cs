using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class ObjectPool<T> where T : class
{
    private Action<T> ResetObj;                           //重置对象
    private Func<T> CreateNewObj;                               //创建新对象
    private List<T> objectList;

    public ObjectPool(Func<T> mNew, Action<T> mReset = null)
    {
        this.CreateNewObj = mNew;
        this.ResetObj = mReset;
        objectList = new List<T>();
    }

    public T New()
    {
        if (objectList.Count == 0)
        {
            T t = CreateNewObj();
            return t;
        }
        else
        {
            T t = objectList[0];
            objectList.RemoveAt(0);
            if(ResetObj!=null)
            {
                ResetObj(t);
            }
            return t;
        }
    }
    public void ReturnObj(T t)
    {
        objectList.Add(t);
    }
    //清空池子
    public void Clear()
    {
        objectList.Clear();
    }

}
