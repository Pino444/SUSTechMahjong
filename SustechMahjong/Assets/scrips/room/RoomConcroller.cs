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
    
    public void onQuitRoomButtonClick()
    {
        quitRoomPanel.SetActive(true);
    }

    public void onYesButtonClick()
    {
        SceneManager.LoadScene("hall");
    }

    public void onNoButtonClick()
    {
        quitRoomPanel.SetActive(false);
    }

    public void onMentorClick()
    {
        chooseMentorPanel.SetActive(false);
    }

    public void onScoreValueChange(float value)
    {
        print(value);
        chooseCourseButton.GetComponent<Text>().text = "投入的积分是：\n"+Convert.ToString((int)value);
    }
}
