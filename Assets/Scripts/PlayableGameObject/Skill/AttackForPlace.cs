using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackForPlace : Skill
{
    public float coolDownTime;
    public float damage;
    private bool isFly;
    private float oldTimeActivate;
    private int count;
    public bool isReady;
    public float distanceRange;
    public GameObject area;
    public GameObject areaRange;
    [SerializeField]private GameObject bomb;

    public GameObject skill;
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
        if (isReady && GamaManager.gm.pc.getSpeed() == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.black);
                if (Vector3.Distance(hit.point, transform.position) <= distanceRange)
                {
                    area.SetActive(true);
                    area.transform.position = new Vector3(hit.point.x, 0.05f, hit.point.z);
                    if (Input.GetMouseButtonUp(1))
                    {
                        GameObject fireballObject =
                            Instantiate(bomb, hit.point + transform.up * 3.5f, Quaternion.identity);
                        fireballObject.SetActive(true);
                        fireballObject.SendMessage("fly", _aliveObject.gameObject);
                        fireballObject.SendMessage("setDamage", Random.Range(damage * GamaManager.gm.pc.getDamage(), damage * GamaManager.gm.pc.getDamage() + GamaManager.gm.pc.level));
                        isActive = false;
                        oldTimeActivate = Time.time;
                        areaRange.SetActive(false);
                        area.SetActive(false);
                        isReady = false;
                    }
                }
                else
                {
                    area.SetActive(false);   
                }
            }
        }
        if (Input.GetMouseButtonDown(0) || GamaManager.gm.pc.getSpeed() > 0)
        {
            isReady = false;
            areaRange.SetActive(false);
            area.SetActive(false);
        }
    }
    
    public override void action(AliveObject aliveObject)
    {
        if (isActive && GamaManager.gm.pc.getSpeed() == 0.0f)
        {
            if (aliveObject.hp > 0)
            {
                _aliveObject = aliveObject;
            }
            isReady = true;
            areaRange.SetActive(true);
            area.SetActive(true);
        }
    }
    
    public override string getInfo()
    {
        return "Fire death " + (damage * GamaManager.gm.pc.minDamage) + "/" + (damage * GamaManager.gm.pc.maxDamage) + " CD " + coolDownTime + " seconds ";
    }
    
    public override string getInfoLevelNext()
    {
        float damageTmp = damage + (damage * 0.1f);
        float coolDownTimeTmp = coolDownTime - (coolDownTime * 0.05f);
            
        return "Fire death " + (damageTmp * GamaManager.gm.pc.minDamage) + "/" + (damageTmp * GamaManager.gm.pc.maxDamage) + " CD " + coolDownTimeTmp + " seconds ";
    }
}
