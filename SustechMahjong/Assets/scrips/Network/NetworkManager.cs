using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networkManeger : MonoBehaviour
{
    // Start is called before the first frame update
    NetworkCore networkCore;
    // Use this for initialization
    void Start () {
        networkCore = GetComponent<NetworkCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTestClick()
    {
        networkCore.Test();
    }
}
