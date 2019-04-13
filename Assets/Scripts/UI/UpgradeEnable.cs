using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEnable : MonoBehaviour
{
    public Skill skill;

    public GameObject buttonUpgrade;
    
    public Text text;
    
    // Update is called once per frame
    void Update()
    {
        if (skill.levelSkill == skill.maxLvlSkill)
        {
            buttonUpgrade.SetActive(false);
        }
        else if (GamaManager.gm.pc.getAmountPointTalent() != 0)
        {
            buttonUpgrade.SetActive(true);
        }
        else
        {
            buttonUpgrade.SetActive(false);
        }
        text.text = "L." + skill.levelSkill;
    }
}
