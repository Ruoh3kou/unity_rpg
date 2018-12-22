using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagInfo : MonoBehaviour
{
    public int[] Itemlist = new int[14];
    public int[] ItemNum = new int[14];

    private static BagInfo instance;
    public static BagInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(BagInfo)) as BagInfo;

                if (instance == null)
                    Debug.LogError("没找到BagInfo的实例。");
            }
            return instance;
        }
    }
    public GameObject bag;
    public GameObject[] cell = new GameObject[14];
    public GameObject[] btn = new GameObject[14];
    //游戏物品
    public Sprite empty;//0
    public Sprite AnimalSkin;//1
    public Sprite SmallFlower;//2
    public Sprite SkinCloth;//3


    void Start()
    {
        Init_bag();
    }
    void Update()
    {
        Load_Itemlist();
    }
    void Init_bag()
    {
        cell[0] = GameObject.Find("Canvas/Bag/cell_1");
        cell[1] = GameObject.Find("Canvas/Bag/cell_2");
        cell[2] = GameObject.Find("Canvas/Bag/cell_3");
        cell[3] = GameObject.Find("Canvas/Bag/cell_4");
        cell[4] = GameObject.Find("Canvas/Bag/cell_5");
        cell[5] = GameObject.Find("Canvas/Bag/cell_6");
        cell[6] = GameObject.Find("Canvas/Bag/cell_7");
        cell[7] = GameObject.Find("Canvas/Bag/cell_8");
        cell[8] = GameObject.Find("Canvas/Bag/cell_9");
        cell[9] = GameObject.Find("Canvas/Bag/cell_10");
        cell[10] = GameObject.Find("Canvas/Bag/cell_11");
        cell[11] = GameObject.Find("Canvas/Bag/cell_12");
        cell[12] = GameObject.Find("Canvas/Bag/cell_13");
        cell[13] = GameObject.Find("Canvas/Bag/cell_14");

        btn[0] = GameObject.Find("Canvas/Bag/cell_1/Button_1");
        btn[1] = GameObject.Find("Canvas/Bag/cell_2/Button_2");
        btn[2] = GameObject.Find("Canvas/Bag/cell_3/Button_3");
        btn[3] = GameObject.Find("Canvas/Bag/cell_4/Button_4");
        btn[4] = GameObject.Find("Canvas/Bag/cell_5/Button_5");
        btn[5] = GameObject.Find("Canvas/Bag/cell_6/Button_6");
        btn[6] = GameObject.Find("Canvas/Bag/cell_7/Button_7");
        btn[7] = GameObject.Find("Canvas/Bag/cell_8/Button_8");
        btn[8] = GameObject.Find("Canvas/Bag/cell_9/Button_9");
        btn[9] = GameObject.Find("Canvas/Bag/cell_10/Button_10");
        btn[10] = GameObject.Find("Canvas/Bag/cell_11/Button_11");
        btn[11] = GameObject.Find("Canvas/Bag/cell_12/Button_12");
        btn[12] = GameObject.Find("Canvas/Bag/cell_13/Button_13");
        btn[13] = GameObject.Find("Canvas/Bag/cell_14/Button_14");
        bag = GameObject.Find("Bag");
        bag.SetActive(false);
    }
    void Load_Itemlist()
    {
        for (int i = 0; i < 14; i++)
        {
            if (Itemlist[i] == 0)
            {
                btn[i].GetComponent<Image>().sprite = empty;
            }
            else if (Itemlist[i] == 1)
            {
                btn[i].GetComponent<Image>().sprite = AnimalSkin;
            }
            else if (Itemlist[i] == 2)
            {
                btn[i].GetComponent<Image>().sprite = SmallFlower;
            }
            else if (Itemlist[i] == 3)
            {
                btn[i].GetComponent<Image>().sprite = SkinCloth;
            }
        }
    }
}
