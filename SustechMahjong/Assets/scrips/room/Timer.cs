
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
    private int statement = -1;//1: 打牌超时； 2 选导师超时； 3 投积分超时 4 吃碰杠胡超时 5 积分选牌超时
    void Start()
    {
        _text = timerText.GetComponent<Text>();
        Times = 10;
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

    public void setStatment(int state)
    {
        statement = state;
    }

    public int getStatement()
    {
        return statement;
    }
}
