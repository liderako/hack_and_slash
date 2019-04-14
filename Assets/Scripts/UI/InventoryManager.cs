using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int weaponsNmber = 0;
    public GameObject[] cells;
    private PlayerController pc;
    public bool isInvertoryActive;
    public GameObject invertoryPanel;
    public GameObject cellUnderMouse;
    public bool mouseOutsideInvertory;
    private GamaManager gm;
    void Start()
    {
        cellUnderMouse = null;
        isInvertoryActive = false;
        pc = GameObject.Find("Maya").GetComponent<PlayerController>();
        gm = GameObject.Find("GamaManager").GetComponent<GamaManager>();
        foreach (var cell in cells)
        {
            if (cell.GetComponent<InventoryCell>().currentWeapon)
                weaponsNmber++;
        }
    }

    public void ChangeInventoryStatus()
    {
        gm.isStaticPlayer = !gm.isStaticPlayer;
        invertoryPanel.SetActive(gm.isStaticPlayer);
    }

    public void changeToWeapon(WeaponStats stats)
    {
        pc.ChangeWeapon(stats);
    }

    public void putWeaponOnTheGround(GameObject weapon)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject weap = Instantiate(weapon, hit.point, Quaternion.identity);
            weap.transform.localScale *= 7;
            if ((int) weap.GetComponent<WeaponStats>().weaponType == 1)
            {
                Vector3 euler = weap.transform.eulerAngles;
                euler.x += 90;
                weap.transform.eulerAngles = euler;
            }
        }
    }

    public void TakeItem(GameObject weap)
    {
        foreach (var cell in cells)
        {
            if (cell.GetComponent<InventoryCell>().currentWeapon == null)
            {
                cell.GetComponent<InventoryCell>().TakeNewItem(weap.GetComponent<WeaponStats>());
                break;
            }
        }
    }
    
    private void Update()
    {
        int newNumber = 0;
        foreach (var cell in cells)
        {
            if (cell.GetComponent<InventoryCell>().currentWeapon != null)
                newNumber++;
        }
        if (weaponsNmber != newNumber)
            weaponsNmber = newNumber;
    }
}
