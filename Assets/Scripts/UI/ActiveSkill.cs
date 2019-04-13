using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public int number;
    
    // Update is called once per frame
    void Update()
    {
        if (SkillManager.sk.skill[number] != null)
        {
            if (SkillManager.sk.skill[number].isActive)
            {
                gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }
}
