using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerController : AliveObject
{
    public bool isRun;
    public bool isEnemy;
    public bool isAttackEnd;
    public GameObject point;
    public int upgradePoint;
    public float nextLevelXP;
    public MouseTarget mouseTarget;
    
    [SerializeField]private LayerMask _layerMask;
    [SerializeField]private EnemyController _targetEnemy;
    [SerializeField]private ParticleSystem _particalSystemLevelUp;
    [SerializeField]private float distanceRange;
    
    private NavMeshAgent agent;
    private Animator animator;
    private int amountPointTelent;
    
    public void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        updateState();
    }
    
    void Update()
    {
        if (hp > 0)
        {
            if (!GamaManager.gm.isStaticPlayer)
            {
                move();
            }
            if (Input.GetMouseButtonUp(0) && isEnemy)
            {
                isAttackEnd = true;
                
            }
            else if (Input.GetMouseButton(0) && isEnemy)
            {
                attackAnimationStart();
            }
            passiveSkills();
        }
        if (hp <= 0)
        {
            dead();
        }
    }

    void move()
    {
        if (Input.GetMouseButton(0) && !isEnemy)
        {
            Vector3 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 10 && Vector3.Distance(hit.transform.position, transform.position) <= distanceRange)
                {
                    EnemyController tmp = hit.transform.gameObject.GetComponent<EnemyController>();
                    if (75 + agility - tmp.agility > 50)
                    {
                        if (!tmp.isDead && tmp.isSpawn)
                        {
                            _targetEnemy = tmp;
                            mouseTarget.target = _targetEnemy;
                            mouseTarget.isTarget = true;
							transform.LookAt(_targetEnemy.gameObject.transform.position);
                            attackAnimationStart();
                        }
                    }
                }
                else if (hit.transform.gameObject.layer != 5)
                {
                    _moveOnPosition(hit.point);
                }
            }
        }
		animator.SetFloat("Speed", agent.velocity.magnitude);

    }

    public void animationRun(bool status)
    {
        animator.SetBool("Run", status);
        isRun = status;
    }

    public void hit(float damage)
    {
        hp -= Convert.ToInt32(damage * (1 - armor / 200));
        if (hp < 0)
        {
            hp = 0;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
	
        if (other.gameObject.tag == "HitPoint")
        {
            increaselifePotion();
            Destroy(other.gameObject);
        }
    }

    public void increaselifePotion()
    {
        hp += Convert.ToInt32(maxHp * 0.3f);
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public float getDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }

    public int getAmountPointTalent()
    {
        return amountPointTelent;
    }

    public void increasePointTalent()
    {
        amountPointTelent--;
    }
    
    /*
    * For animation event
    */
    public void deadEndAnimation()
    {
        return;
    }
    
    /*
     * For animation event
     */
    public void attack()
    {
        if (isAttackEnd)
        {
            attackAnimationStop();
            mouseTarget.isTarget = false;
            mouseTarget.target = null;
            isAttackEnd = false;
        }
        _targetEnemy.hit(getDamage());
        if (_targetEnemy.hp <= 0 && _targetEnemy.isExp)
        {
            isAttackEnd = true;
            isEnemy = false;
            increaseExp(_targetEnemy.exp);
            _targetEnemy.isExp = false;
        }
    }
    
    private void _moveOnPosition(Vector3 hit)
    {
		transform.LookAt(hit);
        agent.SetDestination(hit);
        point.transform.position = hit;
    }

    public void cheatLevelUp()
    {
        increaseExp(nextLevelXP);
    }
    
    private void increaseExp(float exp)
    {
        this.exp += exp;
        if (this.exp >= nextLevelXP)
        {
            level += 1;
            hp = maxHp;
            upgradePoint += 5;
            amountPointTelent += 1;
            this.exp = this.exp - nextLevelXP;
            double _nextLevelXp = System.Convert.ToDouble(nextLevelXP * 1.5);
            nextLevelXP = System.Convert.ToInt32(Math.Ceiling(_nextLevelXp));
            _particalSystemLevelUp.Play();
        }
    }
    
    private void attackAnimationStart()
    {
        animator.SetBool("attack", true);
        isEnemy = true;
    }
    
    private void attackAnimationStop()
    {
        animator.SetBool("attack", false);
        isEnemy = false;
    }
    
    private void dead()
    {
        animator.SetBool("Dead", true);
    }
}
