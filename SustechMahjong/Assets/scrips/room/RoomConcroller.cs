using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomConcroller : MonoBehaviour
{
    public GameObject quitRoomButton;
    public GameObject quitRoomPanel;
    public GameObject chooseMentorPanel;
    public GameObject chooseCourseButton;
    public GameObject networkManeger;
    public GameObject readyButton;
    public GameObject readyState;
    private void Awake()
    {
        Debug.Log("awake run");
        networkManeger = GameObject.Find("Network");
    }
    
    public void onQuitRoomButtonClick()
    {
        quitRoomPanel.SetActive(true);
    }

    public void onYesButtonClick()
    {
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "quitroom"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"room",PlayerPrefs.GetString("room")},
            {"room_id",PlayerPrefs.GetString("room_id")}
        });
        SceneManager.LoadScene("hall");
    }

    public void onNoButtonClick()
    {
        quitRoomPanel.SetActive(false);
    }

    public void onMentorClick()
    {
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "supervisor"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"room",PlayerPrefs.GetString("room")},
            {"room_id",PlayerPrefs.GetString("room_id")},
            {"content","1"}
        });
        chooseMentorPanel.SetActive(false);
    }

    public void onScoreValueChange(float value)
    {
        print(value);
        PlayerPrefs.SetString("score",Convert.ToString((int)value));
        chooseCourseButton.GetComponent<Text>().text = "投入的积分是：\n"+Convert.ToString((int)value);
    }

    public void onScoreConfirmButtonClick()
    {
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "score"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"room",PlayerPrefs.GetString("room")},
            {"room_id",PlayerPrefs.GetString("room_id")},
            {"content",PlayerPrefs.GetString("score")}
        });
        GameObject.Find("Slider").GetComponent<Slider>().maxValue -= int.Parse(PlayerPrefs.GetString("score"));
        GameObject.Find("scoreButton").GetComponent<Button>().interactable = false;
    }

    public void onCourseConfirmButtonClick()
    {
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "score"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"room",PlayerPrefs.GetString("room")},
            {"room_id",PlayerPrefs.GetString("room_id")},
            {"content","10"}
        });
    }
    private bool ready = false;
    public void onReadyButtonClick()
    {
        if (ready)
        {
            readyButton.transform.Find("Text").GetComponent<Text>().text = "准备";
            readyState.SetActive(false);
            networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
            {
                {"type", "cancelready"},
                {"socket_id", PlayerPrefs.GetString("socket_id")},
                {"room",PlayerPrefs.GetString("room")},
                {"room_id",PlayerPrefs.GetString("room_id")}
            });
            ready = false;
        }
        else
        {
            readyButton.transform.Find("Text").GetComponent<Text>().text = "取消";
            readyState.SetActive(true);
            networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
            {
                {"type", "ready"},
                {"socket_id", PlayerPrefs.GetString("socket_id")},
                {"room",PlayerPrefs.GetString("room")},
                {"room_id",PlayerPrefs.GetString("room_id")}
            });
            ready = true;
        }
    }

    public void onChiButtonClick()
    {
        
    }
}
