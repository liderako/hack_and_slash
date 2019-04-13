using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    public virtual void action(GameObject gameObject)
    {
        Debug.Log("PassiveSkill");
    }
}
