using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConcroller : MonoBehaviour
{
    public InputField nickName;
    public Text remind;
    public GameObject networkManeger;

    private void Awake()
    {
        Debug.Log("awake run");
        networkManeger = GameObject.Find("Network");
    }

    public void setNameButtonPress()
    {
        //判断昵称后跳转到大厅
        String nickName = this.nickName.text;
        if (nickName == "")
        {
            //昵称是空的，提示用户
            print("name is none");
            remind.gameObject.SetActive(true);
            StartCoroutine(killReminder());
        }
        else
        {
            //进入大厅
//            networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
//            {
//                {"type", "name"},
//                {"socket_id", PlayerPrefs.GetString("socket_id")},
//                {"content",nickName}
//            });
            PlayerPrefs.SetString("nickName",nickName);
            SceneManager.LoadScene("hall");
        }
    }

    IEnumerator killReminder()
    {
        yield return new WaitForSeconds(1);
        remind.gameObject.SetActive(false);
    }
}
