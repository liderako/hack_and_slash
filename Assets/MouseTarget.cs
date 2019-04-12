using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    public GamaManager gm;
    
    private RaycastHit hit;

    private Ray ray;

    private Vector3 mouse;

    public bool isTarget;

    public EnemyController target;

    public void Start()
    {
        Vector3 mouse = new Vector3();
    }
    
    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        mouse.z = 10;
        ray = Camera.main.ScreenPointToRay(mouse);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 10)
            {
                gm.target(hit.transform.gameObject.GetComponent<EnemyController>());
            }
            else if (isTarget)
            {
                gm.target(target);
                if (target.hp <= 0)
                {
                    isTarget = false;
                    target = null;
                }
            }
            else 
            {
                gm.targetOff();
            }
        }
    }
}
