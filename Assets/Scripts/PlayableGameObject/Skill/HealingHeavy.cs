using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHeavy : Skill
{
    public int levelSkill;
    public float coolDownTime;
    public float powerRegen;
    
    [SerializeField]private bool isActive;
    private float oldTimeActivate;
    [SerializeField] private ParticleSystem _ps;
    
    public void levelUpSkill()
    {
        levelSkill += 1;
        powerRegen = powerRegen + (powerRegen * 0.075f);
        coolDownTime = coolDownTime - (coolDownTime * 0.05f);
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
            float hp = aliveObject.hp;
            float maxHp = aliveObject.maxHp;
            if (hp > 0)
            {
                hp = hp + (maxHp * powerRegen);
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
                aliveObject.hp = System.Convert.ToInt32(hp);
            }
            isActive = false;
            oldTimeActivate = Time.time;
            _ps.Play();
        }
    }
}
