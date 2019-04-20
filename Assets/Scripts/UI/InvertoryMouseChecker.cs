using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvertoryMouseChecker : MonoBehaviour
{
    private InventoryManager _im;

    private void Start()
    {
        Debug.Log(transform.position);
        _im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if ((Input.mousePosition.x > transform.position.x && Input.mousePosition.x < transform.position.x + rect.rect.width) &&
            (Input.mousePosition.y < transform.position.y && Input.mousePosition.y > transform.position.y - rect.rect.height))
        {
            _im.mouseOutsideInvertory = false;
        }
        else
        {
            _im.mouseOutsideInvertory = true;
        }
    }
}
