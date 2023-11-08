using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public float width;
    public float height;

    public Room(float width,float height)
    {
        this.width = width;
        this.height = height;
    }

    public Vector3 ReturnPosInRoom(Vector3 pos)
    {
        return new Vector3(Mathf.Clamp(pos.x, -width / 2, width / 2), Mathf.Clamp(pos.y, -height / 2, height / 2), 0);
    }

    public Vector3 ReturnRandomPosInRoom()
    {
        return new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2),0);
    }

    //返回以Pos为圆心的随机位置
    public Vector3 ReturnRandomPosAround(Vector3 pos,float minRadius,float maxRadius)
    {
        float radius = Random.Range(minRadius, maxRadius);
        float r = Mathf.Sqrt(Random.Range(0, radius));
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r,0);
        newPos += pos;
        return ReturnPosInRoom(newPos);
    }
}
