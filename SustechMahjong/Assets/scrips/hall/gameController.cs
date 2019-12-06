using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject setting;
    public GameObject introText;
    
    private GameObject occupy = null;
    public void onSettingButtonClick()
    {
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
}
