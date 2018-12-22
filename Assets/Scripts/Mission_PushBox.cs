using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_PushBox : MonoBehaviour
{
    private static Mission_PushBox instance;
    public static Mission_PushBox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Mission_PushBox)) as Mission_PushBox;

                if (instance == null)
                    Debug.LogError("没找到Mission_PushBox的实例。");
            }
            return instance;
        }
    }
    public static bool PushBox_Isfinished = false;

    public GameObject flower;
    public GameObject box;
    public GameObject glass;
    public GameObject fence;//栅栏
    public GameObject fence_left;
    public GameObject fence_right;
    public GameObject smallflower;
    public GameObject Restart;
    private GameObject restart;
    private GameObject[] flowers = new GameObject[3];
    private GameObject[] glasses = new GameObject[3];
    private Vector3 newPos1;
    private Vector3 newPos2;
    private Vector3 boxPos;
    public int cover = 0;
    private GameObject[] Fence_up = new GameObject[19];
    private GameObject[] Fence_left = new GameObject[6];
    private GameObject[] Fence_down = new GameObject[19];
    private GameObject[] Fence_right = new GameObject[6];

    public bool Can_restart = false;
    public bool Can_start = false;

    private void Start()
    {
        this.flower = (GameObject)Resources.Load("Prefabs/flower");
        this.box = GameObject.Find("Box");
        this.glass = (GameObject)Resources.Load("Prefabs/glass");
        this.fence = (GameObject)Resources.Load("Prefabs/栅栏front_0");
        this.fence_left = (GameObject)Resources.Load("Prefabs/栅栏left_0");
        this.fence_right = (GameObject)Resources.Load("Prefabs/栅栏right_0");
        this.smallflower = (GameObject)Resources.Load("Prefabs/唤醒花");
        this.Restart = (GameObject)Resources.Load("Prefabs/开关左_0");
        boxPos = this.box.transform.position;
    }

    public void overBoxMission()
    {
        for (int i = 0; i < 19; i++)
        {
            GameObject.Destroy(Fence_up[i]);
            GameObject.Destroy(Fence_down[i]);
            if (i < 6)
            {
                GameObject.Destroy(Fence_left[i]);
                GameObject.Destroy(Fence_right[i]);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject.Destroy(glasses[i]);
            GameObject.Destroy(flowers[i]);
        }
        GameObject.Destroy(restart);
        GameObject.Destroy(box.gameObject);
        GameObject.Instantiate(smallflower, new Vector3(box.transform.position.x + 1, box.transform.position.y, box.transform.position.z), Quaternion.identity);

    }

    public void doBoxMission()
    {
        if (Can_restart)
            restartBoxMission();
        if (Can_start)
            initBoxMission();
        if (Mission_PushBox.instance.cover == 3)
            overBoxMission();

    }

    public void initBoxMission()
    {
        Can_start = false;
        newPos1 = new Vector3(boxPos.x - 9, boxPos.y + 4, 1.0f);
        newPos2 = new Vector3(boxPos.x - 9, boxPos.y - 4, 1.0f);
        for (int i = 1; i < 19; i++)
        {
            Fence_up[i] = GameObject.Instantiate(fence, newPos1, Quaternion.identity);
            Fence_down[i] = GameObject.Instantiate(fence, newPos2, Quaternion.identity);
            newPos1 = Fence_up[i].transform.position;
            newPos1.x = boxPos.x - 9 + i * 1.1f;
            newPos1.y = boxPos.y + 4;
            newPos1.z = 1.0f;

            newPos2 = Fence_down[i].transform.position;
            newPos2.x = boxPos.x - 9 + i * 1.1f;
            newPos2.y = boxPos.y - 4;
            newPos2.z = 1.0f;
        }
        newPos1 = new Vector3(boxPos.x - 9, boxPos.y - 2.7f, 1.0f);
        newPos2 = new Vector3(boxPos.x + 10, boxPos.y - 2.7f, 1.0f);
        for (int i = 0; i < 6; i++)
        {
            Fence_left[i] = GameObject.Instantiate(fence_left, newPos1, Quaternion.identity);
            Fence_right[i] = GameObject.Instantiate(fence_right, newPos2, Quaternion.identity);
            newPos1 = Fence_left[i].transform.position;
            newPos1.x = boxPos.x - 9;
            newPos1.y = boxPos.y - 2.7f + i * 1.3f;
            newPos1.z = 1.0f;

            newPos2 = Fence_right[i].transform.position;
            newPos2.x = boxPos.x + 10;
            newPos2.y = boxPos.y - 2.7f + i * 1.3f;
            newPos2.z = 1.0f;
        }
        for (int i = 0; i < 3; i++)
        {
            flowers[i] = GameObject.Instantiate(flower, new Vector3(Random.Range(boxPos.x - 4, boxPos.x + 4), Random.Range(boxPos.y - 2.5f, boxPos.y + 2.5f), 1f), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            glasses[i] = GameObject.Instantiate(glass, new Vector3(Random.Range(boxPos.x - 4, boxPos.x + 4), Random.Range(boxPos.y - 2.5f, boxPos.y + 2.5f), 1.5f), Quaternion.identity);
        }
        restart = GameObject.Instantiate(Restart, new Vector3(box.transform.position.x + 9, box.transform.position.y + 3, box.transform.position.z), Quaternion.identity);
        box.SetActive(false);
    }
    public void restartBoxMission()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject.Destroy(flowers[i]);
            GameObject.Destroy(glasses[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            flowers[i] = GameObject.Instantiate(flower, new Vector3(Random.Range(boxPos.x - 4, boxPos.x + 4), Random.Range(boxPos.y - 2.5f, boxPos.y + 2.5f), 1), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            glasses[i] = GameObject.Instantiate(glass, new Vector3(Random.Range(boxPos.x - 4, boxPos.x + 4), Random.Range(boxPos.y - 2.5f, boxPos.y + 2.5f), 1), Quaternion.identity);
        }
    }
}