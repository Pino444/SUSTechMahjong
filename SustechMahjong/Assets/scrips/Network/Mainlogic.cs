using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CardObject 
{
    public int id;
    public GameObject card;
    public CardObject(int id,GameObject gob)
    {
        this.id = id;
        this.card = gob;
    }
    public CardObject()
    {
        this.id = 0;
        this.card = null;
    }
    public void moveTo(Vector3 position)
    {
        this.card.transform.localPosition=position;
    }
    
}






public class Mainlogic : MonoBehaviour
{

    
    public GameObject[] cardlist;
    //CardObject[] handcard = new CardObject[13];
    CardObject nowcard;
    playersetting[] players = { new Thisplayer(), new Nextplayer(), new Antiplayer(), new Frontplayer() };
    decodedic de = new decodedic();
    public GameObject name1;
    public GameObject name2;
    public GameObject name3;
    public GameObject name4;

    public GameObject ready1;
    public GameObject ready2;
    public GameObject ready3;
    public GameObject ready4;
    public GameObject networkManeger;
    private void Awake()
    {
        Debug.Log("awake run");
        networkManeger = GameObject.Find("Network");
    }

    private void Update()
    {
//        if (gameObject.GetComponent<NetworkManeger>().dic != null)
//        {
//            excute(gameObject.GetComponent<NetworkManeger>().dic);
//        }
        

//        if (name1 != null)
//        {
//            Text text = name1.transform.Find("Text").GetComponent<Text>();
//            text.text = "ppp";
//        }
        
        
    }

    public void excute(Dictionary<String,String>command)
    {
        de.decodeinstruction(command);
        switch (de.type){
            case "initcard":
                for (int i = 1; i <= 4; i++)
                {
                    int pl = (i + 4 - de.room_id) % 4;
                    int[] plc = new int[9];
                    Array.Copy(de.cards, i * 9-9, plc, 0, 9);
                    givecard(plc, pl);
                }
                break;
            case "askcard":
                int cardid=LeftClick(0);
                networkManeger.GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                {
                    {"type", "playcard"},
                    {"socket_id", PlayerPrefs.GetString("socket_id")},
                    {"room",PlayerPrefs.GetString("room")},
                    {"room_id",PlayerPrefs.GetString("room_id")},
                    {"content",""+cardid}
                });
                break;
            case "card":
                if (de.cards.Length == 9)
                {
                    givecard(de.cards, de.player);
                }else if (de.cards.Length == 2)
                {
                    getscorecard(de.cards, de.player);

                }else if (de.cards.Length == 1)
                {
                    getcard(de.cards[0], de.player);
                }
                break;
            case "specialope":
                
                break;
            case "play":
                dropcard(de.cards[0], de.player);
                break;
            case "cpg":
                outcard(de.cards, de.player);
                break;
            case "roominfo":
                print("roominfo come!");
                foreach (var VARIABLE in de.names)
                {
                    print(VARIABLE);
                }
                name1 = GameObject.Find("Player"); 
                name2 = GameObject.Find("nextPlayer");
                name3 = GameObject.Find("oppositePlayer");
                name4 = GameObject.Find("lastPlayer");
                
                name1.transform.Find("Text").GetComponent<Text>().text = de.names[0];
                name2.transform.Find("Text").GetComponent<Text>().text = de.names[1];
                name3.transform.Find("Text").GetComponent<Text>().text = de.names[2];
                name4.transform.Find("Text").GetComponent<Text>().text = de.names[3];

                ready1 = GameObject.Find("Player"); 
                ready2 = GameObject.Find("nextPlayer");
                ready3 = GameObject.Find("oppositePlayer");
                ready4 = GameObject.Find("lastPlayer");
                ready1.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[0]);
                ready2.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[1]);
                ready3.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[2]);
                ready4.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[3]);
                
                
                break;
            case "id":
                
                break;
            default:
                Debug.Log("what the fuck command!?");
                break;
        }
    }
    
    void givecard(int[] id,int player)//发牌到玩家
    {

        for (int i = 0; i < id.Length; i++)
        {
            players[player].handcard[i] = new CardObject(id[i], Instantiate(cardlist[id[i] / 4],players[player].gethandposorder(i), players[player].faceRotation));
        }
    }

//摸牌
    void getcard(int id,int player)
    {
        nowcard = new CardObject(id, Instantiate(cardlist[id / 4], players[player].getgetpos(), players[player].faceRotation) );
    }

    void getscorecard(int[] id,int player)
    {
        int i = 0;
        for ( i = 0; i < 13; i++)
        {
            if (players[player].handcard[i] == null)
            {
                break;
            }
        }
        players[player].handcard[i] = new CardObject(id[0], Instantiate(cardlist[id[0] / 4], players[player].gethandposorder(i), players[player].faceRotation));
        players[player].handcard[i+1] = new CardObject(id[1], Instantiate(cardlist[id[1] / 4], players[player].gethandposorder(i+i), players[player].faceRotation));
    }

//打出牌
    void dropcard(int id,int player)
    {
        int handpos = gethandpos(id,player);
        if(handpos>=0)
        {
            CardObject drp = players[player].handcard[handpos];
            players[player].handcard[handpos] = nowcard;
            nowcard = drp;
        }
            nowcard.card.transform.localPosition = players[player].dropzone;
            nowcard.card.transform.localRotation= players[player].deskRotation;
        players[player].nextdrop();
            showcard(player);
    }
//吃碰杠
    void outcard(int[] ids,int player)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            int handpos = gethandpos(ids[i],player);
            if (handpos >= 0)
            {
                CardObject drp = players[player].handcard[handpos];
                drp.card.transform.localPosition = players[player].getoutpos();
                drp.card.transform.localRotation = players[player].deskRotation;
                players[player].handcard[handpos] = null;

            }
            else
            {
                nowcard.card.transform.localPosition = players[player].getoutpos();
                nowcard.card.transform.localRotation= players[player].deskRotation;
                players[player].lastdrop();
            }
        }
        showcard(player);
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
                foreach (CardObject c in players[player].handcard)
                {
                    if (c.card == go)
                    {
                        return c.id;
                    }
                }
            }
            else
            {
                return nowcard.id;
            }

        }
        return -1;
    }

    int gethandpos(int id,int player)
    {
        bool inhand = false;
        int handpos = 0;
        foreach (CardObject carob in players[player].handcard)
        {
            if (carob != null)
            {
                if (carob.id == id)
                {
                    inhand = true;
                    break;
                }
               
            }
            handpos++; 
        }
        if (inhand)
        {
             return handpos;
        }           
        else
        {
            return -1;
        }
    }

    void showcard(int player)//重新排序
    {
        reordercard(player);
        int i = 0;
        foreach (CardObject conb in players[player].handcard)
        {
            if (conb == null)
            {
                break;
            }
            conb.card.transform.localPosition = players[player].gethandposorder(i);
            i++;
            conb.card.transform.localRotation = players[player].faceRotation;
        }
    }

    void reordercard(int player) 
    {
        CardObject temp = null;
        int pi=0, pj = 0;
        for (int i = 0; i < players[player].handcard.Length; i++)
        {
            
            for (int j = i+1; j < players[player].handcard.Length; j++)
            {
                if (players[player].handcard[i] == null)
                {
                    pi = 1000;
                }
                else
                {
                    pi = players[player].handcard[i].id;
                }
                if (players[player].handcard[j] == null)
                {
                    pj = 1000;
                }
                else
                {
                    pj = players[player].handcard[j].id;
                }
                if (pi > pj)
                {
                    temp = players[player].handcard[i];
                    players[player].handcard[i] = players[player].handcard[j];
                    players[player].handcard[j] = temp;
                }
            }
        }
    }
       
}
