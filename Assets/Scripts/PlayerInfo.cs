using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(PlayerInfo)) as PlayerInfo;

                if (instance == null)
                    Debug.LogError("没找到PlayerInfo的实例。");
            }
            return instance;
        }
    }

    public bool Can_Dialogues = false;//可否进行对话
    public Dialogue Dia;//对话脚本读取

    public bool Bag_Open = false;//背包是否打开

    public bool Can_Attack = true;//可攻击
    public bool Can_Move = true;//可移动
    public float attackTime = 1;//记录攻击间隔
    public float attackdistance = 6;//记录攻击距离
    public float Player_Attack_num = 2;//玩家的攻击力

    public float Move_Speed = 3;//人物行走速度
    public int Player_AllHp = 6;//玩家的总血量上限
    public int Player_Hp = 6;//玩家的当前血量

    public bool GetFire = false;//是否得到龙了
    public bool GetCloth = false;//是否得到兽皮大衣了
    public bool GetLight = false;//是否亮了
}
