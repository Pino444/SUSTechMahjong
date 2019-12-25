using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public GameObject setting;
    public GameObject introText;
    public GameObject joinRoomPanel;
    public GameObject quitGamePanel;
    public GameObject networkManeger;
    public InputField roomNumber;
    private void Awake()
    {
        Debug.Log("awake run");
        networkManeger = GameObject.Find("Network");
    }

    private GameObject occupy = null;
    public void onSettingButtonClick()
    {
        GameObject.Find("settingButton").GetComponent<AudioSource>().Play();
        if (occupy == null)
        {
            iTween.MoveTo(setting,new Vector3(200,288,0), 1f);
            occupy = setting;
        }
        else if (occupy == setting)
        {
            iTween.MoveTo(occupy,new Vector3(-600,288,0), 1f);
            occupy = null;
        }
        else
        {
            iTween.MoveTo(occupy,new Vector3(-600,288,0), 1f);
            iTween.MoveTo(setting,new Vector3(200,288,0), 1f);
            occupy = setting;
        }
    }

    public void onIntroButtonClick()
    {
        GameObject.Find("introductionButton").GetComponent<AudioSource>().Play();
        if (occupy == null)
        {
            iTween.MoveTo(introText,new Vector3(200,288,0), 1f);
            occupy = introText;
        }
        else if (occupy == introText)
        {
            iTween.MoveTo(occupy,new Vector3(-600,288,0), 1f);
            occupy = null;
        }
        else
        {
            iTween.MoveTo(occupy,new Vector3(-600,288,0), 1f);
            iTween.MoveTo(introText,new Vector3(200,288,0), 1f);
            occupy = introText;
        }
    }

    public void onResetNameButtonClick()
    {
        SceneManager.LoadScene("init");
    }

    public void onCreateRoomButtonClick()
    {
        GameObject.Find("buildRoomButton").GetComponent<AudioSource>().Play();
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "create"},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"content",PlayerPrefs.GetString("nickName")}
        });
        networkManeger.GetComponent<Mainlogic>().refreshScence();
        SceneManager.LoadScene("room1");
    }

    public void onJoinRoomButtonClick()
    {
        joinRoomPanel.SetActive(true);
        GameObject.Find("joinRoomButton ").GetComponent<AudioSource>().Play();
    }

    public void onCloseJoinRoomButtonClick()
    {
        joinRoomPanel.SetActive(false);
    }

    public void onQuitGameButtonClick()
    {
        GameObject.Find("exitButton").GetComponent<AudioSource>().Play();
        quitGamePanel.SetActive(true);
    }

    public void onYesButtonClick()
    { 
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "quitGame"},
            {"socket_id", PlayerPrefs.GetString("socket_id")}
        });
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void onNoButtonClick()
    {
        quitGamePanel.SetActive(false);
    }

    public void onJoinRoomConfirmButtonClick()
    {
        String Number = this.roomNumber.text;
        print("roomNumber" + Number);
        PlayerPrefs.SetString("room",Number);
        networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
        {
            {"type", "joinroom"},
            {"room",Number},
            {"socket_id", PlayerPrefs.GetString("socket_id")},
            {"content",PlayerPrefs.GetString("nickName")}
        });
        networkManeger.GetComponent<Mainlogic>().refreshScence();
        SceneManager.LoadScene("room1");
    }
    
    public void onAudioValueChange(float value)
    {
        //改变音量
        GameObject.Find("Network").GetComponent<AudioSource>().volume = value;
    }

//    private bool isPlaying = true;
    public void onMusicToggleChange(bool value)
    {
        if (value == true)
        {
            GameObject.Find("Network").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("Network").GetComponent<AudioSource>().Stop();
        }
        
    }
}
