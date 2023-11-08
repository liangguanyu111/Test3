using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//暂时只配一个触发次数和动画名字
public class Skill 
{
    public int skillCount;
    public string animationName;

    public Skill(int skillCount,string animation)
    {
        this.skillCount = skillCount;
        this.animationName = animation;
    }
}
