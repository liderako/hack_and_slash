using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager sk;
    public AliveObject pc;
    
    public List<Skill> skill;

    public bool     isDrop;
    public int    number;

    void Awake () {
        if (sk == null)
            sk = this;
    }

    void Update()
    {
        if (GamaManager.gm.isStaticPlayer)
        {
            if (GamaManager.gm.isSelectedSkill)
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    skill[0] = GamaManager.gm.dragSkill.skill;
                    isDrop = true;
                }
//                else if (Input.GetKeyUp(KeyCode.Alpha2))
//                {
//                    //skill[1].action(pc);
//                }
//                else if (Input.GetKeyUp(KeyCode.Alpha3))
//                {
//                    //skill[2].action(pc);
//                }
//                else if (Input.GetKeyUp(KeyCode.Alpha4))
//                {
//                    //Debug.Log("Alpha 4");
//                }
            }
        }
        else
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
}
