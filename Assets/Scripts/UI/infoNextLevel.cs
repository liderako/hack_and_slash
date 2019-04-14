using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class infoNextLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Skill skill;
    public GameObject _text;
    public GameObject fon;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        fon.SetActive(true);
        _text.GetComponent<Text>().text = skill.getInfoLevelNext();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fon.SetActive(false);
    }
}
