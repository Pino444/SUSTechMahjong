using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getName : MonoBehaviour
{
    public Text name;
    // Start is called before the first frame update
    void Start()
    {
        String nickName = PlayerPrefs.GetString("nickName");
        name.text = nickName;
    }
    
}
