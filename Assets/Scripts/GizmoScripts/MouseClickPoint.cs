using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickPoint : MonoBehaviour
{
    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "enemy.png", false);
    }
    
}
