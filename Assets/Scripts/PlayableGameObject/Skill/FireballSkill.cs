﻿using System.Collections;
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
    
    private AliveObject _aliveObject;
    
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
                _aliveObject = aliveObject;
            }
            GameObject fireballObject = Instantiate(fireball, transform.position, Quaternion.identity);
            fireballObject.SendMessage("fly", aliveObject.gameObject);
            isActive = false;
            oldTimeActivate = Time.time;
        }
    }
}
