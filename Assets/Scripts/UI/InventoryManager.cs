using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> weaponsInInventory;
    private PlayerController pc;
    public bool isInvertoryActive;
    public GameObject invertoryPanel;
    private GamaManager gm;
    void Start()
    {
        isInvertoryActive = false;
        pc = GameObject.Find("Maya").GetComponent<PlayerController>();
        gm = GameObject.Find("GamaManager").GetComponent<GamaManager>();
        weaponsInInventory = new List<GameObject>();
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
}
