using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateAttack : Skill
{
    public float coolDownTime;
    public float damage;
    private bool isRotate;
    private float oldTimeActivate;
    private int count;
    
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
        if (isRotate)
        {
            _aliveObject.transform.rotation = Quaternion.Euler(0, _aliveObject.transform.rotation.y + 10 * count, 0);
            count += 3;
        }
        if (count >= 65)
        {
            count = 0;
            isRotate = false;
            explosionDamage();
        }
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
            isActive = false;
            isRotate = true;
            oldTimeActivate = Time.time;
        }
    }
    
    void explosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_aliveObject.transform.position, 0.4f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Equals("EnemyObject"))
            {
                hitColliders[i].SendMessage("hit", damage * _aliveObject.GetComponent<PlayerController>().getDamage());
            }
            i++;
        }
    }
}
