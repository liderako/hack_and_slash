using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        if (speed == 0)
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        else
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * speed);
        }
    }
}
