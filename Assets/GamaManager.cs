using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    public Text textXp;
    public Text textHp;
    public Text textLevel;
    public Slider hpSlider;
    public Slider xpSlider;
    public PlayerController pc;

    public GameObject panelPlayer;
    public GameObject panelGameOver;
    public GameObject panelCharacter;
    
    public Slider hpEnemy;
    public Text textHpEnemy;
    public Text textTargetEnemyName;
    public Text textLvlEnemy;

    public List<GameObject> _enemyView;

    private bool swap;

    public Text textName;
    public Text textStrengh;
    public Text textAgility;
    public Text textConstitution;
    public Text textArmor;
    public Text textUpgradePoints;
    public Text textMinMaxDamage;
    public Text textMaxHp;
    public Text textEXpCharacter;
    public Text textXpToNextLevel;
    public Text textCredits;

    public GameObject upgradeButton;

    public GameObject upgradeButtonStrange;

    public GameObject upgradeButtonAgility;

    public GameObject upgradeButtonCON;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            onSwap();
        }

        if (swap)
        {
            textName.text = "Maya Lv." + pc.level;
            textStrengh.text = "Strengh: " + pc.strengh;
            textAgility.text = "Agility: " + pc.agility;
            textConstitution.text = "Constitution: " + pc.constitution;
            textArmor.text = "Armor" + pc.armor;
            textUpgradePoints.text = "Upgrade Points: " + pc.upgradePoint;
            textMinMaxDamage.text = "Min-Max Damage: " + pc.minDamage + "-" + pc.maxDamage;
            textMaxHp.text = "Max Hp: " + pc.maxHp;
            textEXpCharacter.text = "EXP:" + pc.exp;
            textXpToNextLevel.text = "Exp to next level: " + pc.nextLevelXP;
            textCredits.text = "Credits: " + pc.credits;
        }
        if (pc.hp > 0)
        {
            textHp.text = pc.hp.ToString();
            textLevel.text = "Lv." + pc.level;
            textXp.text = pc.exp + "/" + pc.nextLevelXP;
            hpSlider.maxValue = pc.maxHp;
            hpSlider.value = pc.hp;
            xpSlider.maxValue = pc.nextLevelXP;
            xpSlider.value = pc.exp;
        }
        else
        {
            panelPlayer.SetActive(false);
            panelGameOver.SetActive(true);
        }

        if (pc.upgradePoint > 0)
        {
            upgradeButton.SetActive(true);
            upgradeButtonStrange.SetActive(true);
            upgradeButtonAgility.SetActive(true);
            upgradeButtonCON.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
            upgradeButtonStrange.SetActive(false);
            upgradeButtonAgility.SetActive(false);
            upgradeButtonCON.SetActive(false);
        }
    }

    public void target(EnemyController enemy)
    {
        foreach (var view in _enemyView)
        {
            view.SetActive(true);
        }
        hpEnemy.maxValue = enemy.maxHp;
        hpEnemy.value = enemy.hp;
        textHpEnemy.text = enemy.hp.ToString();
        textLvlEnemy.text = "Lv." + enemy.level;
        textTargetEnemyName.text = enemy._type;
        
    }

    public void upgradeStreght()
    {
        pc.strengh += 1;
        pc.updateDamage();
        pc.upgradePoint--;
    }

    public void upgradeAgility()
    {
        pc.agility += 1;
    }

    public void upgradeCON()
    {
        pc.constitution += 1;
        pc.updateHp();
        pc.upgradePoint--;
    }

    public void onSwap()
    {
        if (!swap)
        {
            panelCharacter.SetActive(true);
            swap = true;
        }
        else
        {
            panelCharacter.SetActive(false);
            swap = false;
        }
    }

    public void targetOff()
    {
        foreach (var view in _enemyView)
        {
            view.SetActive(false);
        }
    }
}
