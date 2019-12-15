using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getRoomNumber : MonoBehaviour
{
    public Text roomNumber;
    void Start()
    {
        String room = PlayerPrefs.GetString("room");
        roomNumber.text = "房间号："+room;
    }
}
