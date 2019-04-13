using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager sk;
    public AliveObject pc;
    
    public List<Skill> skill;
    public List<int> _isN;

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
                    number = 0;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    skill[1] = GamaManager.gm.dragSkill.skill;
                    isDrop = true;
                    number = 1;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    skill[2] = GamaManager.gm.dragSkill.skill;
                    isDrop = true;
                    number = 2;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    skill[3] = GamaManager.gm.dragSkill.skill;
                    isDrop = true;
                    number = 3;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (skill[0] != null)
                {
                    skill[0].action(pc);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                if (skill[1] != null)
                {
                    skill[1].action(pc);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                if (skill[2] != null)
                {
                    skill[2].action(pc);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                if (skill[3] != null)
                {
                    skill[3].action(pc);
                }
            }
        }
    }
}
