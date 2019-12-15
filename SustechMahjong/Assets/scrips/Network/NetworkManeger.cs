using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManeger : MonoBehaviour
{
    NetworkCore networkCore;
//    Mainlogic _mainlogic = new Mainlogic();
    public static NetworkManeger instance;

    public Dictionary<String, String> dic;
    //全局唯一性  
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("严重 : 对象已经存在!");
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    void Start () {
        networkCore = GetComponent<NetworkCore>();
        networkCore.SetupConnection();
    }

    // Update is called once per frame
    void Update()
    {
        startRecive();
    }

    public void sendMsg(Dictionary<string, string> dict)
    {
        networkCore.SendData(dict);
    }

    public void startRecive()
    {
//        while (true)
//        {
            if (networkCore.msgDict.Count != 0)
            {
                Debug.Log("队列长度"+networkCore.msgDict.Count);
                Dictionary<String,String> command = networkCore.msgDict.Dequeue();
                dic = command;
                gameObject.GetComponent<Mainlogic>().excute(command);
//                _mainlogic.excute(command);
            }
//        }
    }
}
