using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : Skill
{
    public float coolDownTime;
    public float damage;
    private bool isFly;
    private float oldTimeActivate;
    private int count;
    [SerializeField]private GameObject fireball;
    
    //private AliveObject _aliveObject;
    
    public void levelUpSkill()
    {
        if (levelSkill < maxLvlSkill)
        {
            levelSkill += 1;
            damage = damage + (damage * 0.1f);
            coolDownTime = coolDownTime - (coolDownTime * 0.05f);
            GamaManager.gm.upgradeSkillDone();
        }
    }

    public void Update()
    {
        if (!isActive && Time.time - oldTimeActivate > coolDownTime)
        {
            isActive = true;
        }
    }
    
    public override void action(AliveObject aliveObject)
    {
        if (isActive) {
            if (aliveObject.hp > 0)
            {
                GameObject fireballObject = Instantiate(fireball, transform.position, Quaternion.identity);
                fireballObject.SendMessage("fly", gameObject);
                isActive = false;
                oldTimeActivate = Time.time;
            }
        }
    }
    
    public override string getInfo()
    {
        return "Fireball damage" + (damage + GamaManager.gm.pc.minDamage * GamaManager.gm.pc.level) + "/" + (damage + GamaManager.gm.pc.maxDamage * GamaManager.gm.pc.level) + " CD " + coolDownTime + " seconds ";
    }

    public override string getInfoLevelNext()
    {
        float damageTmp = damage + (damage * 0.1f);
        float coolDownTimeTmp = coolDownTime - (coolDownTime * 0.05f);
            
        return "Fireball damage" + (damageTmp + GamaManager.gm.pc.minDamage * GamaManager.gm.pc.level) + "/" + (damageTmp + GamaManager.gm.pc.maxDamage * GamaManager.gm.pc.level) + " CD " + coolDownTimeTmp + " seconds ";
    }
}
