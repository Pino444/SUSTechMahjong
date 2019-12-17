using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardactivity : MonoBehaviour
{
    bool can=false;
    public Vector3 self;
    public bool first;
    int _id=0;
    bool dapai=false;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        self = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (can)
        {
            checkmousein();
            if (dapai)
            {
                LeftClick();

            }
            
        }
    }


    public void setdapai()
    {
        dapai = true;
    }

    public void setcan(bool bol)
    {
        can = bol;
    }

    public bool getcan()
    {
        return can;
    }

    public void setid(int id)
    {
        _id = id;
    }

    private void OnEnter()
    {

       
        Vector3 v3 = self;
        v3.y += 0.005f;
        v3.z -= 0.01f;
        gameObject.transform.localPosition = v3;

    }

    private void OnExit()
    {

        gameObject.transform.localPosition = self;
    }
    void checkmousein()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameObject go = hit.collider.gameObject;

            if ( first && go == gameObject)
            {
                first = false;
                OnEnter();
            } else if (!first &&  go!=gameObject)
            {
                first = true;
                OnExit();
            }

        }
        else if (!first )
        {
            first = true;
            OnExit();
        }



    }

    public int LeftClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                GameObject go = hit.collider.gameObject;
                if (go == gameObject)
                {
                    print(_id);
                    GameObject.Find("Network").GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "playcard"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content",""+_id}
                    });
                    dapai = false;
                    GameObject.Find("TimerImage").gameObject.SetActive(false);
                    return _id;
                }
            }
            

        }
        return -1;
    }

}