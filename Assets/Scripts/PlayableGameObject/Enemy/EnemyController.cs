using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AliveObject
{

    public bool isPlayer;
    public bool isAttack;
    [HideInInspector]public bool isSpawn;
    [HideInInspector]public bool isDead;
    [HideInInspector]public bool isExp;
    
    public GameObject playerObject;
    public PlayerController playerController;
    public GameObject loot;

    
    private NavMeshAgent agent;
    private Animator _animator;
    private CapsuleCollider capsuleCollider;
    private Vector3 checkDeadPoint;
    private Vector3 checkPoint;

    [HideInInspector]public GameObject _parent;
    [SerializeField] private string _type;
    [SerializeField] private float _speedDownDead;
    [SerializeField] private float _speedUpSpawn;
    

    /*
     * Unity api methods
     */
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        checkPoint = _parent.GetComponent<EnemySpawner>().enemyCheckPointUp.transform.position;
        updateState();
    }

    void Update()
    {
        if (hp > 0)
        {
            aliveLogic();
            passiveSkills();
        }
        else if (hp <= 0)
        {
            deadLogic();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hp > 0)
        {
            targetOnPlayer(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && hp > 0 && isPlayer && playerController.hp <= 0)
        {
            isPlayer = false;
            isAttack = false;
            agent.enabled = false;
            _animator.SetBool("attack", false);
        }
    }

    /*
     * Public action
     */
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
    
    public void hit(float damageTmp)
    {
        if (!isDead)
        {
            hp -= System.Convert.ToInt32(damageTmp * (1 - armor / 200));
            if (hp <= 0)
            {
                hp = 0;
                isExp = true;
                _animator.SetBool("Dead", true);
                isDead = true;
                spawnLoot();
            }
        }
        if (!isPlayer)
        {
            findPlayer();
        }
    }
    
    void findPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Equals("Player"))
            {
                targetOnPlayer(hitColliders[i].gameObject);
            }
            i++;
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

    public string GetTypeObject()
    {
        return _type;
    }
    
    /*
     * Private action
     */
    private void deadLogic()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkDeadPoint, _speedDownDead * Time.deltaTime);
        if (Vector3.Distance(transform.position, checkDeadPoint) < 0.0001f)
        {
            _parent.GetComponent<EnemySpawner>().isEnemy = false;
            Destroy(gameObject);
                
        }
    }

    private void aliveLogic()
    {
        if (!isSpawn)
        {
            spawnMove();
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

    private void spawnMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkPoint, _speedUpSpawn * Time.deltaTime);
        if (Vector3.Distance(transform.position, checkPoint) < 0.0001f)
        {
            isSpawn = true;
            agent.Warp(transform.position);
        }
    }
    
    private void spawnLoot()
    {
        if (Random.Range(0, 15) > 10)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
    
    private void rotateForAttack()
    {
        Vector3 difference = playerObject.gameObject.transform.position - transform.position; 
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        agent.transform.rotation = Quaternion.Euler(0f, -rotation_y + 90, 0);
        
    }
    
    private void targetOnPlayer(GameObject targetPlayer)
    {
        if (targetPlayer.GetComponent<PlayerController>().hp > 0)
        {
            playerObject = targetPlayer.gameObject;
            playerController = targetPlayer.gameObject.GetComponent<PlayerController>();
            isPlayer = true;
        }
    }

    /*
     * Animation event
     */
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
}
