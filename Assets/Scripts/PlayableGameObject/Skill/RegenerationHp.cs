using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationHp : PassiveSkill
{
    private float oldTime;
    public float coolDownTime;
    public float powerRegen;

    public void levelUpSkill()
    {
        if (levelSkill < maxLvlSkill)
        {
            levelSkill += 1;
            coolDownTime = coolDownTime - (coolDownTime * 0.10f);
            GamaManager.gm.upgradeSkillDone();
        }
    }
    
    public override void action(GameObject gameObject)
    {
        if (Time.time - oldTime > coolDownTime)
        {
            float hp = gameObject.GetComponent<AliveObject>().hp;
            float maxHp = gameObject.GetComponent<AliveObject>().maxHp;
            if (hp > 0)
            {
                hp = hp + (maxHp * powerRegen);
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
                gameObject.GetComponent<AliveObject>().hp = System.Convert.ToInt32(hp);
            }
            oldTime = Time.time;
        }
    }
}
