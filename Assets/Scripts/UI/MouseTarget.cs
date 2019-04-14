using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    
    
    private RaycastHit hit;

    private Ray ray;

    private Vector3 mouse;

    public bool isTarget;

    public AliveObject target;

    public void Start()
    {
        mouse = new Vector3();
    }
    
    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        mouse.z = 10;
        ray = Camera.main.ScreenPointToRay(mouse);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "EnemyObject")
            {
                GamaManager.gm.target(hit.transform.gameObject.GetComponent<AliveObject>());
            }
            else if (isTarget)
            {
                GamaManager.gm.target(target);
                if (target.hp <= 0)
                {
                    isTarget = false;
                    target = null;
                }
            }
            else 
            {
                GamaManager.gm.targetViewEnemy(false);
            }
        }
    }
}
