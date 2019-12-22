using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class couserController : MonoBehaviour,IPointerClickHandler
{
    int card1;
    int card2;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            print("" + card1);
            print("" + card2);

            string courenumber=gameObject.name.Substring(gameObject.name.Length - 1, 1);
            GameObject.Find("Network").GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
            {
                {"type", "choice"},
                {"socket_id", PlayerPrefs.GetString("socket_id")},
                {"room",PlayerPrefs.GetString("room")},
                {"room_id",PlayerPrefs.GetString("room_id")},
                {"content",courenumber}
            });
            gameObject.transform.parent.gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("TimerImage").gameObject.SetActive(false);
        }
        

    }
    public void setTwoCard(int a,int b)
    {
        card1 = a;
        card2 = b;
    }
}