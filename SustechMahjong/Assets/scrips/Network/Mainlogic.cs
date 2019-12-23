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
        this.card.GetComponent<cardactivity>().setid(id);
    }
    public CardObject()
    {
        this.id = 0;
        this.card = null;
    }

}






public class Mainlogic : MonoBehaviour
{

    
    public GameObject[] cardlist;

    public Sprite[] cardImage;
    //CardObject[] handcard = new CardObject[13];
    CardObject nowcard;
    playersetting[] players = { new Thisplayer(), new Nextplayer(), new Antiplayer(), new Frontplayer() };
    
    public decodedic de; 
    public GameObject name1;
    public GameObject name2;
    public GameObject name3;
    public GameObject name4;
    private GameObject chosepanel;
    public GameObject networkManeger;
    public GameObject roomText;
    public GameObject canvas;
    public GameObject cpgh;
    public Button chibut;
    public Animator anim;
    Image timei;

    GameObject eswn;
    private void Awake()
    {
        Debug.Log("awake run");
        networkManeger = GameObject.Find("Network");
        de= new decodedic();
    }

    public void excute(Dictionary<String,String>command)
    {
        if (nowcard != null)
        {
            Debug.Log("nowcard:");
            Debug.Log(nowcard.id);
            Debug.Log(nowcard.card);

        }
        
        de.decodeinstruction(command);
        switch (de.type){
            case "initcard":
                GameObject.Find("Canvas").transform.Find("readyButton").gameObject.SetActive(false);
                for (int i = 1; i <= 4; i++)
                {
                    int pl = (i + 4 - de.room_id) % 4;
                    int[] plc = new int[9];
                    Array.Copy(de.cards, i * 9-9, plc, 0, 9);
                    givecard(plc, pl);
                }
                setcardstatement();
                canvas = GameObject.Find("Canvas");
                canvas.transform.Find("chooseMentorPanel").gameObject.SetActive(true);
                timei = canvas.transform.Find("TimerImage").GetComponent<Image>();
                timei.gameObject.SetActive(true);
                timei.gameObject.transform.Find("TimerText").GetComponent<Timer>().setStatment(2);
                break;
            case "askcard"://服务器让你给他传要打的牌
                if (de.content.Equals("" + de.room_id))
                {
                    setdapai();
                    canvas = GameObject.Find("Canvas");
                    timei = canvas.transform.Find("TimerImage").GetComponent<Image>();
                    timei.gameObject.SetActive(true);
                    timei.gameObject.transform.Find("TimerText").GetComponent<Timer>().setStatment(1);
                    if (nowcard != null)
                    {
                        nowcard.card.GetComponent<cardactivity>().setcan(true);
                    }
                    
                }
                
                
                break;
            case "college":
                int coll1 = de.cards[0]+26;
                int coll2 = de.cards[1]+26;
                canvas = GameObject.Find("Canvas");
                canvas.transform.Find("disappearCollege1").gameObject.SetActive(true);
                canvas.transform.Find("disappearCollege2").gameObject.SetActive(true);
                canvas.transform.Find("disappearCollege1").GetComponent<Image>().sprite = cardImage[coll1];
                canvas.transform.Find("disappearCollege2").GetComponent<Image>().sprite = cardImage[coll2];
                
                break;
            case "card"://服务器给你传谁拿了什么牌
                if (de.cards.Length == 2)
                {
                    string cbtn = "courseButton";
                    for (int i = 0; i < 4; i++)
                    {
                        if (de.cardheapnum==i)
                        {
                            int num = i + 1;
                            int[] scorecard = {de.pairlist[i * 2], de.pairlist[i * 2 + 1]};
                            getscorecard(scorecard, de.player);
                            string findstr = cbtn + num;
                            canvas = GameObject.Find("Canvas");
                            Button butn = canvas.transform.Find("chooseCoursePanel").transform.Find(findstr).GetComponent<Button>();
                            butn.transform.Find("Text").gameObject.SetActive(true);

                        }
                        
                    }
                    showcard(de.player);

                }else if (de.cards.Length == 1)
                {
                    
                    getcard(de.cards[0], de.player);
                }
                break;
            case "specialope":
                canvas = GameObject.Find("Canvas");
                cpgh=canvas.transform.Find("cpgh").gameObject;
                cpgh.SetActive(true);
                chibut=cpgh.transform.Find("chiButton").GetComponent<Button>();
                Button peng = cpgh.transform.Find("pengButton4").GetComponent<Button>();
                Button gang = cpgh.transform.Find("gangButton5").GetComponent<Button>();
                Button hu = cpgh.transform.Find("huButton6").GetComponent<Button>();
                Button cancel = cpgh.transform.Find("cancelButton0").GetComponent<Button>();
                chibut.gameObject.SetActive(de.butt[0]|de.butt[1]|de.butt[2]);
                peng.gameObject.SetActive(de.butt[3]);
                gang.gameObject.SetActive(de.butt[4]);
                hu.gameObject.SetActive(de.butt[5]);
                cancel.gameObject.SetActive(de.butt[0]|de.butt[1]|de.butt[2]|de.butt[3]|de.butt[4]|de.butt[5]);
                if (!(de.butt[0]|de.butt[1]|de.butt[2]|de.butt[3]|de.butt[4]|de.butt[5]))
                {
                    System.Threading.Thread.Sleep(500);
                    GameObject.Find("Network").GetComponent<NetworkManeger>().sendMsg(new Dictionary<string, string>()
                    {
                        {"type", "opereply"},
                        {"socket_id", PlayerPrefs.GetString("socket_id")},
                        {"room",PlayerPrefs.GetString("room")},
                        {"room_id",PlayerPrefs.GetString("room_id")},
                        {"content","0"}
                    });

                }
                else
                {
                    timei = canvas.transform.Find("TimerImage").GetComponent<Image>();
                    timei.gameObject.SetActive(true);
                    timei.gameObject.transform.Find("TimerText").GetComponent<Timer>().setStatment(4);
                }
                
                break;
            case "play":
                if (de.player == 0)
                {
                    anim = GameObject.Find("Hand1").GetComponent<Animator>();
                    anim.SetTrigger(name: "takeTrigger");
                    Debug.Log("anim Play!");
                    dropcard(de.cards[0], de.player);
                    nowcard.card.SetActive(false);
                    CardObject p = nowcard;
                    StartCoroutine(delayPlayCard(p));
                    break;
                }
                dropcard(de.cards[0], de.player);
                
                break;
            case "cpg":
                outcard(de.cards, de.player);
                break;
            case "roominfo":
                ESWN(de.room_id);
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

                name1.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[0]);
                name2.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[1]);
                name3.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[2]);
                name4.transform.Find("ready").GetComponent<Image>().gameObject.SetActive(de.readystatement[3]);

                roomText = GameObject.Find("roomNumberText");
                roomText.GetComponent<Text>().text = de.room;
                
                break;
            case "end":
                canvas = GameObject.Find("Canvas");
                canvas.transform.Find("winPanel").gameObject.SetActive(true);
                Image wp = canvas.transform.Find("winPanel").GetComponent<Image>();
                string endname = "UserImage";
                string endscore = "scoreImage";
                for (int i = 1; i <= 4; i++)
                {
                    print(endname + i);
                    Image imge = wp.transform.Find(endname + i).GetComponent<Image>();
                    imge.transform.Find("Text").GetComponent<Text>().text = de.playernames[i - 1];
                    imge = wp.transform.Find(endscore + i).GetComponent<Image>();
                    imge.transform.Find("Text").GetComponent<Text>().text = de.finalscore[i - 1];
                }

                break;
            case "hu":
                canvas = GameObject.Find("Canvas");
                Image hpl = canvas.transform.Find("huPanel").GetComponent<Image>();
                hpl.gameObject.SetActive(true);
                Image img = hpl.transform.Find("leftImage").GetComponent<Image>();
                img.transform.Find("nameText").GetComponent<Text>().text = de.playernames[0];
                img.transform.Find("scoreText").GetComponent<Text>().text = de.gerenscore;
                string[] cardtype = de.content.Split(' ');
                img = hpl.transform.Find("baseImage").GetComponent<Image>();
                string ppp = "Image";
                for (int i = 0; i < de.cards.Length; i++)
                {
                    img.transform.Find(ppp + (i + 1)).GetComponent<Image>().sprite = cardImage[de.cards[i] / 4];
                }
                ppp = "";
                string right = "";
                for (int i = 0; i < cardtype.Length; i++)
                {
                    if (i < 5)
                    {
                        ppp += cardtype[i];
                        ppp+= "\n";
                    }
                    else
                    {
                        right += cardtype[i];
                        right += "\n";
                    }
                }
                hpl.transform.Find("huText").GetComponent<Text>().text = ppp;
                hpl.transform.Find("huText2").GetComponent<Text>().text = right;


                break;

            case "id":
                
                break;
            case "pair":
                de.whichispicked = new bool[4];
                canvas = GameObject.Find("Canvas");
                canvas.transform.Find("chooseCoursePanel").gameObject.SetActive(true);
                GameObject.Find("chooseCoursePanel").transform.Find("remindText").gameObject.SetActive(false);
                chosepanel = GameObject.Find("chooseCoursePanel");
                GameObject.Find("scoreButton").GetComponent<Button>().interactable = true;
                Button btn = GameObject.Find("courseButton1").GetComponent<Button>();
                btn.interactable = false;
                btn.transform.Find("Image1").GetComponent<Image>().sprite = cardImage[de.cards[0] / 4];
                btn.transform.Find("Image2").GetComponent<Image>().sprite = cardImage[de.cards[1] / 4];
                btn.transform.Find("Text").gameObject.SetActive(false);
    
                btn.GetComponent<couserController>().setTwoCard(de.cards[0],de.cards[1]);

                btn = GameObject.Find("courseButton2").GetComponent<Button>();
                btn.interactable = false;
                btn.transform.Find("Image1").GetComponent<Image>().sprite = cardImage[de.cards[2] / 4];
                btn.transform.Find("Image2").GetComponent<Image>().sprite = cardImage[de.cards[3] / 4];
                btn.transform.Find("Text").gameObject.SetActive(false);
                btn.GetComponent<couserController>().setTwoCard(de.cards[2], de.cards[3]);

                btn = GameObject.Find("courseButton3").GetComponent<Button>();
                btn.interactable = false;
                btn.transform.Find("Image1").GetComponent<Image>().sprite = cardImage[de.cards[4] / 4];
                btn.transform.Find("Image2").GetComponent<Image>().sprite = cardImage[de.cards[5] / 4];
                btn.transform.Find("Text").gameObject.SetActive(false);
                btn.GetComponent<couserController>().setTwoCard(de.cards[4], de.cards[5]);

                btn = GameObject.Find("courseButton4").GetComponent<Button>();
                btn.interactable = false;
                btn.transform.Find("Image1").GetComponent<Image>().sprite = cardImage[de.cards[6] / 4];
                btn.transform.Find("Image2").GetComponent<Image>().sprite = cardImage[de.cards[7] / 4];
                btn.transform.Find("Text").gameObject.SetActive(false);
                btn.GetComponent<couserController>().setTwoCard(de.cards[6], de.cards[7]);

                timei = canvas.transform.Find("TimerImage").GetComponent<Image>();
                timei.gameObject.SetActive(true);
                timei.gameObject.transform.Find("TimerText").GetComponent<Timer>().setStatment(3);
                break;
            case "askchoice":
                if (de.content.Equals("" + de.room_id))
                {
                    string cbtn = "courseButton";
                    GameObject.Find("chooseCoursePanel").transform.Find("remindText").gameObject.SetActive(true);
                    for (int i = 0; i < 4; i++)
                    {
                        if(!de.whichispicked[i])
                        {
                            int mun = i + 1;
                            string findstr = cbtn + mun;
                            Button button=GameObject.Find(findstr).GetComponent<Button>();
                            button.interactable = true;
                        }
                        
                    }
                    canvas = GameObject.Find("Canvas");
                    timei = canvas.transform.Find("TimerImage").GetComponent<Image>();
                    timei.gameObject.SetActive(true);
                    timei.gameObject.transform.Find("TimerText").GetComponent<Timer>().setStatment(5);

                }
                

                break;
            default:
                Debug.Log("what the fuck command!?");
                break;
        }
    }

    void printsomething()
    {
        print("operation player:" + de.player);
        string a = "";
        foreach (CardObject co in players[0].handcard)
        {
            if (co == null)
            {
                break;
            }
            a += (" "+co.id+",");
        }
        print(a);
    }
    
   
    void givecard(int[] id,int player)//发牌到玩家
    {

        for (int i = 0; i < id.Length; i++)
        {
            players[player].handcard[i] = new CardObject(id[i], Instantiate(cardlist[id[i] / 4],players[player].gethandposorder(i), players[player].faceRotation));
        }
        showcard(player);
    }

    void setcardstatement()
    {
        foreach (CardObject c in players[0].handcard)
        {
            if (c == null)
            {
                break;
            }
            c.card.GetComponent<cardactivity>().setcan(true);
        }
    }

    void setdapai()
    {
        foreach (CardObject c in players[0].handcard)
        {
            if (c == null)
            {
                break;
            }
            c.card.GetComponent<cardactivity>().setdapai(true);

        }
        if (nowcard!=null &&nowcard.card.transform.localPosition == players[0].getpos)
        {
            nowcard.card.GetComponent<cardactivity>().setdapai(true);
        }
    }

    public void setdapaifalse()
    {
        foreach (CardObject c in players[0].handcard)
        {
            if (c == null)
            {
                break;
            }
            c.card.GetComponent<cardactivity>().setdapai(false);

        }
        if (nowcard!=null &&nowcard.card.transform.localPosition == players[0].getpos)
        {
            nowcard.card.GetComponent<cardactivity>().setdapai(false);
        }
    }
//摸牌
    void getcard(int id,int player)
    {
        nowcard = new CardObject(id, Instantiate(cardlist[id / 4], players[player].getgetpos(), players[player].faceRotation) );
        if (de.player == 0)
        {
            nowcard.card.GetComponent<cardactivity>().setcan(true);
        }
        
        
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
        print("cardget:"+id[0]+" "+id[1]+"pos:"+i+" "+(i+1));
        players[player].handcard[i] = new CardObject(id[0], Instantiate(cardlist[id[0] / 4], players[player].gethandposorder(i), players[player].faceRotation));
        players[player].handcard[i+1] = new CardObject(id[1], Instantiate(cardlist[id[1] / 4], players[player].gethandposorder(i+1), players[player].faceRotation));
        setcardstatement();
        showcard(player);

    }

//打出牌
    void dropcard(int id,int player)
    {
        Debug.Log(id);
        int handpos = gethandpos(id,player);
        if(handpos>=0)
        {
            if (nowcard.card.transform.localPosition == players[player].getpos)
            {
                CardObject drp = players[player].handcard[handpos];
                players[player].handcard[handpos] = nowcard;
                nowcard = drp;
            }
            else
            {
                CardObject drp = players[player].handcard[handpos];
                players[player].handcard[handpos] =null;
                nowcard = drp;
            }
            
        }
            nowcard.card.transform.localPosition = players[player].dropzone;
            nowcard.card.transform.localRotation= players[player].deskRotation;
        nowcard.card.GetComponent<cardactivity>().setcan(false);

        players[player].nextdrop();
            showcard(player);

    }
    IEnumerator delayPlayCard(CardObject p)
    {
        yield return new WaitForSeconds(0.95f);
        p.card.SetActive(true);
    }
    //吃碰杠
    void outcard(int[] ids,int player)
    {
        Debug.Log(player);
        for (int i = 0; i < ids.Length; i++)
        {
            int handpos = gethandpos(ids[i],player);
            if (handpos >= 0)
            {
                CardObject drp = players[player].handcard[handpos];
                drp.card.transform.localPosition = players[player].getoutpos();
                Debug.Log(players[player].outzone);
                drp.card.transform.localRotation = players[player].deskRotation;
                drp.card.GetComponent<cardactivity>().setcan(false);
                players[player].handcard[handpos] = null;

            }
            else
            {
                nowcard.card.transform.localPosition = players[player].getoutpos();
                nowcard.card.transform.localRotation= players[player].deskRotation;
                nowcard.card.GetComponent<cardactivity>().setcan(false);
                Debug.Log(players[player].outzone);
                players[de.lastplayer].lastdrop();
            }
        }
        showcard(player);
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
        printsomething();
        reordercard(player);
        int i = 0;
        foreach (CardObject conb in players[player].handcard)
        {
            if (conb == null)
            {
                break;
            } 
            Vector3 v3=players[player].gethandposorder(i);
            conb.card.transform.localPosition = v3;
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

    void ESWN(int room_id)
    {
        eswn=GameObject.Find("DemoScene").transform.Find("MahJongDesk").transform.Find("ESWN").gameObject;
        switch (room_id)
        {
            case 1:
                eswn.transform.localRotation=new Quaternion(0,0.7f,0,0.7f);
                break;
            case 2:
                eswn.transform.localRotation=new Quaternion(0,1,0,0);
                break;
            case 3:
                eswn.transform.localRotation=new Quaternion(0,-0.7f,0,0.7f);
                break;
            case 4:
                eswn.transform.localRotation=new Quaternion(0,0,0,0);
                break;
                
        }
    }
    
}
