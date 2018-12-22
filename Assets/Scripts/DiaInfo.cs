using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaInfo : MonoBehaviour {
    private static DiaInfo instance;
    public static DiaInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(DiaInfo)) as DiaInfo;

                if (instance == null)
                    Debug.LogError("没找到DiaInfo的实例。");
            }
            return instance;
        }
    }
    public static GameObject roleA;
    public static GameObject roleB;
    public static GameObject roleName;
    public static GameObject detail;
    public static GameObject dialogue_panel; //对话UI
    void Start () {
        roleA = GameObject.Find("Canvas/dialogus/roleA");
        roleB = GameObject.Find("Canvas/dialogus/roleB");
        roleName = GameObject.Find("Canvas/dialogus/Image/roleName");
        detail = GameObject.Find("Canvas/dialogus/Image/detail");
        dialogue_panel = GameObject.Find("Canvas/dialogus");
        dialogue_panel.SetActive(false);
    }
}
