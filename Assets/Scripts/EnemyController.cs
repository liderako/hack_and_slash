using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AliveObject
{

    public bool isPlayer;
    public bool isAttack;
    public bool isSpawn;
    public bool isDead;
    public bool isExp;
    public GameObject playerObject;

    public GameObject loot;

    private NavMeshAgent agent;

    private Animator _animator;
    
    public PlayerController playerController;

    public CapsuleCollider capsuleCollider;
    
    public Vector3 checkPoint;

    public GameObject _parent;

    public Vector3 checkDeadPoint;

    public string _type;
    

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        checkPoint = _parent.GetComponent<EnemySpawner>().enemyCheckPointUp.transform.position;
        updateState();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            if (!isSpawn)
            {
                transform.position = Vector3.MoveTowards(transform.position, checkPoint, 0.1f * Time.deltaTime);
                if (Vector3.Distance(transform.position, checkPoint) < 0.0001f)
                {
                    isSpawn = true;
                    agent.Warp(transform.position);
                }
            }
            else if (isPlayer)
            {
                agent.SetDestination(playerObject.transform.position);
                _animator.SetBool("Run", true);
                if (!isAttack)
                {
                    _animator.SetBool("Run", true);
                }

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    _animator.SetBool("Run", false);
                    _animator.SetBool("attack", true);
                    isAttack = true;
                    rotateForAttack();
                }

                if (agent.remainingDistance >= agent.stoppingDistance)
                {
                    isAttack = false;
                    _animator.SetBool("attack", false);
                }
            }
        }
        else if (hp <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, checkDeadPoint, 0.01f * Time.deltaTime);
            if (Vector3.Distance(transform.position, checkDeadPoint) < 0.0001f)
            {
                _parent.GetComponent<EnemySpawner>().isEnemy = false;
                Destroy(gameObject);
                
            }
        }
    }

    public void attack()
    {
        if (75 + agility - playerController.agility > 60)
        {
            playerController.hit(Random.Range(minDamage, maxDamage));
            if (playerController.hp < 0)
            {
                isPlayer = false;
            }
        }
    }
    
    public void hit(float damage)
    {
        if (!isDead)
        {
            hp -= System.Convert.ToInt32(damage * (1 - armor / 200));
            if (hp <= 0)
            {
                hp = 0;
                isExp = true;
                _animator.SetBool("Dead", true);
                isDead = true;
                spawnLoot();
            }
        }
    }

    private void spawnLoot()
    {
        if (Random.Range(0, 15) > 10)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
    
    public void upgradeStat()
    {
        strengh = strengh + (strengh * 0.15f);
        constitution = constitution + (constitution * 0.15f);
        agility = agility + (agility * 0.15f);
        exp = exp + (exp * 0.15f);
        updateHp();
        updateDamage();
    }
    
    private void rotateForAttack()
    {
        Vector3 difference = playerObject.gameObject.transform.position - transform.position; 
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        agent.transform.rotation = Quaternion.Euler(0f, -rotation_y + 90, 0);
        
    }

    private void deadEndAnimation()
    {
        isDead = true;
        isAttack = false;
        isPlayer = false;
        isSpawn = false;
        agent.enabled = false;
        capsuleCollider.enabled = false;
        checkDeadPoint = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hp > 0)
        {
            playerObject = other.gameObject;
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.hp > 0)
                isPlayer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && hp > 0 && isPlayer && playerController.hp <= 0)
        {
            isPlayer = false;
            isAttack = false;
            agent.enabled = false;
            _animator.SetBool("attack", false);
        }
    }
}
