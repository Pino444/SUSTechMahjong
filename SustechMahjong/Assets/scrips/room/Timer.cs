using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float Times=10;
    public GameObject timerText;
    public GameObject timer;
    private Text _text;

    void Start()
    {
        _text = timerText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Times -= Time.deltaTime;
        _text.text = "" + (int) Times;
        if (Times < 0)
        {
            timer.SetActive(false);
        }
    }
}
