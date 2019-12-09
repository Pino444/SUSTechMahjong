using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MentorController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

{
    public GameObject text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.SetActive(true);
        Debug.Log("enter!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        Debug.Log("exit!");
    }
    
}
