﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float Times=10;
    public GameObject timerText;
    public GameObject timer;
    private Text _text;
    public GameObject networkManeger;
    private int statement = -1;//1: 打牌超时； 2 选导师超时； 3 投积分超时 4 吃碰杠胡超时 5 积分选牌超时
    void Start()
    {
        networkManeger = GameObject.Find("Network");
        _text = timerText.GetComponent<Text>();
        Times = 10;
    }
    void OnEnable()
    {
        Times = 10;
    }


    // Update is called once per frame
    void Update()
    {
        Times -= Time.deltaTime;
        _text.text = "" + (int) Times;
        if (Times < 0)
        {
            switch (statement)
            {
                case 1:
                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "playcard"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content","-1"}
                    });
                    break;
                case 2://TODO
                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "supervisor"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content","1"}
                        
                    });
                    break;
                case 3:
                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "score"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content","1"}
                    });
                    break;
                case 4:
                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "opereply"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content","0"}
                    });
                    break;
                case 5://TODO
//                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
//                    {
//                        {"type", "quitroom"},
//                        {"socket_id", PlayerPrefs.GetString("socket_id")},
//                        {"room",PlayerPrefs.GetString("room")},
//                        {"room_id",PlayerPrefs.GetString("room_id")}
//                    });
                    break;
                default:
                    Debug.Log("No such Statement!");
                    break;
            }
            timer.SetActive(false);
        }
    }

    public void setStatment(int state)
    {
        statement = state;
    }

    public int getStatement()
    {
        return statement;
    }

}
