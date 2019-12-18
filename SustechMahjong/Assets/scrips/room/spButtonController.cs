using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class spButtonController : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        string mentornumber = gameObject.name.Substring(gameObject.name.Length - 1, 1);
        GameObject.Find("Network").GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "opereply"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"room",PlayerPrefs.GetString("room")},
            {"room_id",PlayerPrefs.GetString("room_id")},
            {"content",mentornumber}
        });
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}