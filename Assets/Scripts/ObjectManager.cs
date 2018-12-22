using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public static ObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ObjectManager)) as ObjectManager;

                if (instance == null)
                    Debug.LogError("没找到DiaInfo的实例。");
            }
            return instance;
        }
    }
    // 照相机
    public GameObject mainCamera;

    // 音乐
    public AudioClip bossBgm;
    // npc
    public GameObject npc_dragon;
    public GameObject npc_egg;
    public GameObject dia_firetree;
    public GameObject npc_villager;
    public GameObject dia_stopcold;
    public GameObject dia_dark;
    public GameObject dia_outdark;
    public GameObject dia_meetBoss;

    // 摇杆 按钮
    public ETCJoystick joystick;
    public ETCButton button_B;
    public ETCButton button_K;
    public ETCButton button_J;

    // 子弹
    public static GameObject player_bullet;//玩家基础子弹

    // UI
    public GameObject Dark_Mesh;
    public GameObject Dark;
    public GameObject DeathUI;
    public GameObject photo;
    public GameObject photo_Big;
    public GameObject playerUI;
    public GameObject[] hp = new GameObject[6];
    public GameObject FinalDia;
    public GameObject bt1;
    public GameObject bt2;
    public GameObject bt3;
    public GameObject bt4;
    public GameObject bt5;


    // wall
    public GameObject blockway;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        npc_egg = GameObject.Find("npc_egg");
        npc_dragon = GameObject.Find("npc_dragon");
        dia_firetree = GameObject.Find("dia_fireTree");
        npc_villager = GameObject.Find("npc_villager");
        dia_stopcold = GameObject.Find("dia_stopcold");
        dia_dark = GameObject.Find("dia_dark");
        dia_outdark = GameObject.Find("dia_outdark");
        dia_meetBoss = GameObject.Find("dia_meetBoss");


        npc_dragon.SetActive(false);

        joystick = ETCInput.GetControlJoystick("Joystick");
        button_B = ETCInput.GetControlButton("Button_B");
        button_J = ETCInput.GetControlButton("Button_J");
        button_K = ETCInput.GetControlButton("Button_K");

        // 记得修改false
        button_J.visible = false;

        player_bullet = (GameObject)Resources.Load("Prefabs/fireball");

        Dark_Mesh = GameObject.Find("DarkMesh");
        Dark = GameObject.Find("Dark");
        Dark_Mesh.SetActive(false);
        Dark.SetActive(false);

        photo = GameObject.Find("Photo");
        photo_Big = GameObject.Find("Photo_Big");
        photo.SetActive(false);
        photo_Big.SetActive(false);

        //DeathUI = GameObject.Find("Death");
        DeathUI.SetActive(false);

        playerUI = GameObject.Find("playerUI");
        playerUI.SetActive(false);

        FinalDia = GameObject.Find("FinalDia");
        bt1 = GameObject.Find("bt1");
        bt2 = GameObject.Find("bt2");
        bt3 = GameObject.Find("bt3");
        bt4 = GameObject.Find("bt4");
        bt5 = GameObject.Find("bt5");
        FinalDia.SetActive(false);
        bt2.SetActive(false);
        bt3.SetActive(false);
        bt4.SetActive(false);
        bt5.SetActive(false);


        hp[0] = GameObject.Find("Canvas/Life/HP1");
        hp[1] = GameObject.Find("Canvas/Life/HP2");
        hp[2] = GameObject.Find("Canvas/Life/HP3");
        hp[3] = GameObject.Find("Canvas/Life/HP4");
        hp[4] = GameObject.Find("Canvas/Life/HP5");
        hp[5] = GameObject.Find("Canvas/Life/HP6");

        blockway = GameObject.Find("blockWay");
        blockway.SetActive(false);
    }

}
