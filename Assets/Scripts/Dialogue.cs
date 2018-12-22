using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Collections.Generic;//用到了容器类  
using UnityEngine.SceneManagement;


public class Dialogue
{
    //这是场景中的各个物体
    private GameObject roleName;
    private GameObject detail;
    private GameObject dialogue_panel; //对话UI

    private List<string> dialogues_list;//存放dialogues的list
    private int dialogue_index;//对话索引
    private int dialogue_count;//对话数量
    private string role;//当前在说话的角色
    private string role_detail;//当前在说话的内容。

    private string diaXML;

    public Dialogue(string diaName)
    {
        diaXML = diaName;
        Init();
    }

    private void Init()
    {

        //变量初始化
        dialogue_index = 0;
        dialogue_count = 0;
        roleName = DiaInfo.roleName;
        detail = DiaInfo.detail;
        dialogue_panel = DiaInfo.dialogue_panel;

        XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”  
        dialogues_list = new List<string>();//初始化存放dialogues的list
        //载入资源文件
        string data = Resources.Load("Dialogues\\" + diaXML).ToString();//注意这里没有后缀名xml。你可以看看编辑器中，也是不带后缀的。因此不要有个同名的其它格式文件注意！
        xmlDocument.LoadXml(data);//载入这个xml  
        XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("dialogues").ChildNodes;//选择<dialogues>为根结点并得到旗下所有子节点  
        foreach (XmlNode xmlNode in xmlNodeList)//遍历<dialogues>下的所有节点<dialogue>压入List
        {
            XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<dialogue>  
            dialogues_list.Add(xmlElement.ChildNodes.Item(0).InnerText + "," + xmlElement.ChildNodes.Item(1).InnerText);
            //将角色名和对话内容存入这个list，中间存个逗号一会儿容易分割
        }
        dialogue_count = dialogues_list.Count;//获取到底有多少条对话
    }

    public void DoDialogue()
    {
        if (dialogue_index < dialogue_count)//如果对话还没有完
        {
            dialogues_handle(dialogue_index);//那就载入下一条对话
            dialogue_index++;//对话跳到一下个
        }
        else

        { //对话完了
          //进入下一游戏场景之类的
            Init();
            PlayerInfo.Instance.Can_Attack = true;
            PlayerInfo.Instance.Can_Move = true;
            PlayerInfo.Instance.Can_Dialogues = false;
            dialogue_panel.SetActive(false);
            if (diaXML == "player2")
            {
                ObjectManager.Instance.npc_egg.SetActive(false);
                ObjectManager.Instance.npc_dragon.SetActive(true);
            }
            if (diaXML == "dragon")
            {
                ObjectManager.Instance.npc_dragon.SetActive(false);
                PlayerInfo.Instance.GetFire = true;
            }
            if (diaXML == "firetree")
            {
                ObjectManager.Instance.button_J.visible = true;
                ObjectManager.Instance.dia_firetree.SetActive(false);
            }
            if (diaXML == "old2")
            {
                PlayerInfo.Instance.GetCloth = true;
            }
            if (diaXML == "dark")
            {
                ObjectManager.Instance.Dark.SetActive(false);
                PlayerInfo.Instance.GetLight = true;
            }
            if (diaXML == "dark2")
            {
                ObjectManager.Instance.dia_dark.SetActive(false);
            }
            if (diaXML == "outdark")
            {
                ObjectManager.Instance.dia_outdark.SetActive(false);
                ObjectManager.Instance.blockway.SetActive(true);
            }
            if (diaXML == "meetboss")
            {
                BossInfo.Instance.watchRange = 9999;
                ObjectManager.Instance.mainCamera.GetComponent<AudioSource>().clip = ObjectManager.Instance.bossBgm;
                ObjectManager.Instance.mainCamera.GetComponent<AudioSource>().Play();
                ObjectManager.Instance.dia_meetBoss.SetActive(false);
            }
            if (diaXML == "photo")
            {
                ObjectManager.Instance.photo.SetActive(false);
                ObjectManager.Instance.photo_Big.SetActive(false);
                ObjectManager.Instance.playerUI.SetActive(true);
                ObjectManager.Instance.joystick.visible = false;
                ObjectManager.Instance.button_B.visible = false;
                ObjectManager.Instance.button_J.visible = false;
                ObjectManager.Instance.FinalDia.SetActive(true);
            }
            if (diaXML == "final1")
            {
                ObjectManager.Instance.bt2.SetActive(true);
            }
            if (diaXML == "final2")
            {
                ObjectManager.Instance.bt2.SetActive(false);
                ObjectManager.Instance.bt3.SetActive(true);
                ObjectManager.Instance.bt4.SetActive(true);
            }
            if (diaXML == "final3")
            {
                SceneManager.LoadScene(0);
            }
            if (diaXML == "final4")
            {
                ObjectManager.Instance.bt3.SetActive(false);
                ObjectManager.Instance.bt4.SetActive(false);
                ObjectManager.Instance.bt5.SetActive(true);
            }
            if (diaXML == "final5")
            {
                SceneManager.LoadScene(0);
            }

        }
    }



    /*处理每一条对话的函数，就是将dialogues_list每一条对话弄到场景*/
    private void dialogues_handle(int dialogue_index)
    {
        //切割数组
        string[] role_detail_array = dialogues_list[dialogue_index].Split(',');//list中每一个对话格式就是“角色名,对话”
        role = role_detail_array[0];
        role_detail = role_detail_array[1];
        switch (role)//根据角色名
        {   //显示当前说话的角色
            case "vivi":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "老欧":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "KK":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "啊螺":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "老欧、村民":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "小超":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            case "？？？":
                roleName.GetComponent<Text>().text = role + ":";
                break;
            default:
                roleName.GetComponent<Text>().text = "";
                break;
        }
        detail.GetComponent<Text>().text = role_detail;//并加载当前的对话
    }

}