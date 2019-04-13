using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager sk;
    public AliveObject pc;

    public List<Skill> skill;
    
    void Awake () {
        if (sk == null)
            sk = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            skill[0].action(pc);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            skill[1].action(pc);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            skill[2].action(pc);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Debug.Log("Alpha 4");
        }
    }
}
