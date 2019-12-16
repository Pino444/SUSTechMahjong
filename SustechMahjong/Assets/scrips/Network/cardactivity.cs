using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardactivity : MonoBehaviour
{
    bool can=false;
    public Vector3 self;
    public bool first;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        self = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        checkmousein();
    }


    public void setcan(bool bol)
    {
        can = bol;
    }

    public bool getcan()
    {
        return can;
    }

    private void OnEnter()
    {
        print("in");
       
        Vector3 v3 = self;
        v3.y += 0.005f;
        v3.z -= 0.01f;
        gameObject.transform.localPosition = v3;

    }

    private void OnExit()
    {
        print("out");
        gameObject.transform.localPosition = self;
    }
    void checkmousein()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GameObject go = hit.collider.gameObject;

            print(go == gameObject);
            if (can && first && go == gameObject)
            {
                first = false;
                OnEnter();
            } else if (!first & can && go!=gameObject)
            {
                first = true;
                OnExit();
            }

        }
        else if (!first & can)
        {
            first = true;
            OnExit();
        }



    }

    public int LeftClick(int player)
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
                    
                }
            }
            else
            {
                return 0;
            }

        }
        return -1;
    }

}