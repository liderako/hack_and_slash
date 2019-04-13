using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Info : MonoBehaviour, IPointerEnterHandler
{

    public Skill skill;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(skill.getInfo());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
