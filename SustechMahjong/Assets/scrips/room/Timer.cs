
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
                    statement = -1;
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
                    GameObject.Find("Canvas").transform.Find("chooseMentorPanel").gameObject.SetActive(false);
                    statement = -1;
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
                    GameObject.Find("Canvas").transform.Find("chooseCoursePanel").transform.Find("Slider").GetComponent<Slider>().maxValue -= 1;
                    GameObject.Find("Canvas").transform.Find("chooseCoursePanel").transform.Find("scoreButton").GetComponent<Button>().interactable = false;
                    statement = -1;
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
                    GameObject.Find("Canvas").transform.Find("cpgh").gameObject.SetActive(false);
                    GameObject.Find("Canvas").transform.Find("mutilChiPanel").gameObject.SetActive(false);
                    statement = -1;
                    break;
                case 5:
                    Image pl=GameObject.Find("Canvas").transform.Find("chooseCoursePanel").GetComponent<Image>();
                    string nm = "courseButton";
                    string send = "";
                    for(int i = 1; i <= 4; i++)
                    {
                        if(pl.transform.Find(nm + i).GetComponent<Button>().interactable){
                            send += i;
                            break;
                        }
                    }
                    networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "choice"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content",send }
                    });
                    GameObject.Find("Canvas").transform.Find("chooseCoursePanel").gameObject.SetActive(false);
                    statement = -1;
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
