using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManeger : MonoBehaviour
{
    NetworkCore networkCore;
    public static NetworkManeger instance;

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
//        networkCore.ReceiveData();
        startRecive();
    }

    public void sendMsg(Dictionary<string, string> dict)
    {
        networkCore.SendData(dict);
    }

    public void startRecive()
    {
        while (true)
        {
            if (networkCore.msgDict.Count != 0)
            {
                Dictionary<String,String> command = networkCore.msgDict.Dequeue();
                commander(command);
            }
        }
    }

    public void commander(Dictionary<String,String> command)
    {
        print(command);
    }
}
