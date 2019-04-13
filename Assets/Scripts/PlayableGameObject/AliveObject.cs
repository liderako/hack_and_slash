using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    public float strengh;//STR
    public float agility;//AGI
    public float constitution;//CON
    public float armor;
    public int hp;
    public int maxHp;
    public float minDamage;
    public float maxDamage;
    public float level;
    public float exp;
    public float credits;
    public List<PassiveSkill> _passiveSkills;

    public void updateState()
    {
        updateHp();
        updateDamage();
    }

    public void updateHp()
    {
        hp = System.Convert.ToInt16(constitution * 5.0f);
        maxHp = hp;
    }

    public void UpdateMaxHp()
    {
        maxHp = System.Convert.ToInt16(constitution * 5.0f);
    }

    public void updateDamage()
    {
        minDamage = strengh / 2;
        maxDamage = minDamage + 4;
    }
    
    public void passiveSkills()
    {
        foreach (var sk in _passiveSkills)
        {
            sk.action(gameObject);
        }
    }
}
