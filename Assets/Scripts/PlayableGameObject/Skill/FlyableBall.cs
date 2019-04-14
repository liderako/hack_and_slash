using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class FlyableBall : MonoBehaviour
{
    public bool isFly;

    public float speed;
    public float distance;
    public Vector3 originVector;
    public float damage;
    
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
            if (Vector3.Distance(originVector, transform.position) >= distance)
            {
                isFly = false;
                Destroy(gameObject);
            }
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
        originVector = go.transform.position;
        dir = go.transform.forward;
        gameObject.tag = go.tag;
        damage = go.GetComponent<FireballSkill>().damage;
        isFly = true;
        isDamage = false;
    }
    
    void explosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.4f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Equals("EnemyObject"))
            {
                hitColliders[i].SendMessage("hit", damage);
                isDamage = true;
            }
            i++;
        }
    }
}
