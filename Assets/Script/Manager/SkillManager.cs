using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager 
{
    public List<Skill> skills = new List<Skill>();

    public void AddSkill(Skill newSkill)
    {
        skills.Add(newSkill);
    }
}
