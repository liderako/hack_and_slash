using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject currentWeapon = null;
    private Image _image;
    public GameObject stats;
    public Text damage;
    public Text speed;
    public Text cost;
    public Image currWeaponImage;
    public Sprite[] weaponImages;
    private InventoryManager _im;
    private void Start()
    {
        _im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _im.cellUnderMouse = gameObject;
        WeaponStats _weaponStats;
        _image.color = Color.green;
        if (currentWeapon)
        {
            _weaponStats = currentWeapon.GetComponent<WeaponStats>();
            damage.text = "Damage: " + _weaponStats.damage;
            speed.text = "Speed: " + _weaponStats.attackSpeed;
            cost.text = "Cost: " + _weaponStats.cost;
            stats.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.white;
        if (currentWeapon)
        {
            stats.SetActive(false);
        }
    }

    private void Update()
    {
        WeaponStats _weaponStats;
        if (currentWeapon)
        {
            _weaponStats = currentWeapon.GetComponent<WeaponStats>();
            currWeaponImage.sprite = weaponImages[(int) _weaponStats.weaponType];
        }
        else
        {
            currWeaponImage.sprite = weaponImages[weaponImages.Length - 1];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentWeapon)
            _im.changeToWeapon(currentWeapon.GetComponent<WeaponStats>());
    }

    public void DropItem()
    {
        if (_im.mouseOutsideInvertory)
        {
            _im.putWeaponOnTheGround(currentWeapon);
            currentWeapon = null;
            stats.SetActive(false);
        }
        else if (_im.cellUnderMouse != gameObject)
        {
            GameObject weaponToPut = currentWeapon;
            currentWeapon = null;
            _im.cellUnderMouse.GetComponent<InventoryCell>().PutItem(weaponToPut, this);
            
        }
    }

    public void PutItem(GameObject weap, InventoryCell cell)
    {
        if(currentWeapon)
            cell.PutItem(currentWeapon, this);
        currentWeapon = weap;
    }
}
