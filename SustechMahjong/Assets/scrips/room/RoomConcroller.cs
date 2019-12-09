using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomConcroller : MonoBehaviour
{
    public GameObject quitRoomButton;
    public GameObject quitRoomPanel;


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
}
