using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int levelSkill;
    public int maxLvlSkill;
    
    public bool isActive;
    
    public virtual void action(AliveObject alive)
    {
    }

    public virtual string getInfo()
    {
        return "null";
    }

    public virtual string getInfoLevelNext()
    {
        return "null";
    }
}
