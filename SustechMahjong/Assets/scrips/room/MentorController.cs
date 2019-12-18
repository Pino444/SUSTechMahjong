using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MentorController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler

{
    public GameObject text;

    public GameObject networkmaneger;

    public void Awake()
    {
        networkmaneger = GameObject.Find("network");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            string mentornumber=gameObject.name.Substring(gameObject.name.Length - 1, 1);
            GameObject.Find("Network").GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
            {
                {"type", "supervisor"},
                {"socket_id", PlayerPrefs.GetString("socket_id")},
                {"room",PlayerPrefs.GetString("room")},
                {"room_id",PlayerPrefs.GetString("room_id")},
                {"content",mentornumber}
            });
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        
    }

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