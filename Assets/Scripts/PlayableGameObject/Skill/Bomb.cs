using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isFly;

    public float speed;
    private float _damage;
    
    private Vector3 dir;
    private Rigidbody _rb;
    private bool isDamage;
    
    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isFly)
        {
            _rb.AddForce(dir * Time.deltaTime * speed, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        if (!isDamage)
        {
            explosionDamage();
        }
    }

    public void fly(GameObject go)
    {
        dir = -go.transform.up;
        gameObject.tag = go.tag;
        isFly = true;
        isDamage = false;
    }

    public void setDamage(float damage)
    {
        _damage = damage;
    }
    
    void explosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.4f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Equals("EnemyObject"))
            {
                hitColliders[i].SendMessage("hit", _damage);
                isDamage = true;
            }
            i++;
        }
    }
}
