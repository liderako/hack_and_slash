using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    public virtual void action(GameObject gameObject)
    {
        Debug.Log("PassiveSkill");
    }
}
