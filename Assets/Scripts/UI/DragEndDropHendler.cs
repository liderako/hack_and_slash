using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEndDropHendler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private InventoryManager _im;
    public InventoryCell cell;
    private void Start()
    {
        _im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cell.currentWeapon != null)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
        cell.DropItem();
    }
}
