using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerController : AliveObject
{
    private NavMeshAgent agent;

    private Animator animator;

    public bool isRun;

    public bool isEnemy;

    public GameObject point;

    public float distanceRange;

    public bool isAttackEnd;

    public int upgradePoint;

    public float nextLevelXP;

    public ParticleSystem _particalSystem;

    public MouseTarget mouseTarget;
    
    [SerializeField]private LayerMask _layerMask;

    [SerializeField]private EnemyController _targetEnemy;

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
            move();
            if (Input.GetMouseButtonUp(0) && isEnemy)
            {
                isAttackEnd = true;
                
            }
            else if (Input.GetMouseButton(0) && isEnemy)
            {
                attackAnimationStart();
            }
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
            mouse.z = 1000;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 10 && Vector3.Distance(hit.point, transform.position) <= distanceRange)
                {
                    EnemyController tmp = hit.transform.gameObject.GetComponent<EnemyController>();
                    if (75 + agility - tmp.agility > 50)
                    {
                        if (!tmp.isDead && tmp.isSpawn)
                        {
                            _targetEnemy = tmp;
                            mouseTarget.target = _targetEnemy;
                            mouseTarget.isTarget = true;
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
        if (agent.remainingDistance <= agent.stoppingDistance && isRun)
        {
            animator.SetBool("Run", false);
            isRun = false;
        }
    }

    public void hit(float damage)
    {
        hp -= System.Convert.ToInt32(damage * (1 - armor / 200));
        if (hp < 0)
        {
            hp = 0;
        }
    }
    
    private void _moveOnPosition(Vector3 hit)
    {
        Vector3 difference = hit - transform.position; 
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        agent.transform.rotation = Quaternion.Euler(0f, -rotation_y + 90, 0);
        agent.SetDestination(hit);
        point.transform.position = hit;
        animator.SetBool("Run", true);
        isRun = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.tag == "HitPoint")
        {
            increaselifePotion();
            Destroy(other.gameObject);
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
    
    void dead()
    {
        animator.SetBool("Dead", true);
    }

    public void deadEndAnimation()
    {
        return;
    }

    public void increaselifePotion()
    {
        hp += Convert.ToInt32(maxHp * 0.3f);
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void attack()
    {
        if (isAttackEnd)
        {
            attackAnimationStop();
            mouseTarget.isTarget = false;
            mouseTarget.target = null;
            isAttackEnd = false;
        }
        _targetEnemy.hit(Random.Range(minDamage, maxDamage));
        if (_targetEnemy.hp <= 0 && _targetEnemy.isExp)
        {
            isAttackEnd = true;
            isEnemy = false;
            increaseExp(_targetEnemy.exp);
            _targetEnemy.isExp = false;
        }
    }

    private void increaseExp(float exp)
    {
        this.exp += exp;
        if (this.exp >= nextLevelXP)
        {
            level += 1;
            hp = maxHp;
            upgradePoint += 5;
            this.exp = this.exp - nextLevelXP;
            double _nextLevelXp = System.Convert.ToDouble(nextLevelXP * 1.5);
            nextLevelXP = System.Convert.ToInt32(Math.Ceiling(_nextLevelXp));
            _particalSystem.Play();
        }
    }
}
