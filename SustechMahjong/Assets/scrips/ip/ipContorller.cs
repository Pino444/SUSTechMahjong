using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ipContorller : MonoBehaviour
{
    public InputField ip;
    
    public void setIPButtonPress()
    {
        //判断昵称后跳转到大厅
        String ip = this.ip.text;
     
        if (ip != "")
        {
            PlayerPrefs.SetString("ip",ip);
            SceneManager.LoadScene("init");
        }
    }
}
