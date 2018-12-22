using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour {
    private static BossInfo instance;
    public static BossInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(BossInfo)) as BossInfo;

                if (instance == null)
                    Debug.LogError("没找到BossInfo的实例。");
            }
            return instance;
        }
    }

    public float watchRange = 0f;//监视范围
    public float attackTime = 1;//记录攻击间隔
    public float Boss_Move_Speed = 1;//boss行走速度
    public float Boss_Catch_Speed = 1;//boss追击速度
    public float Boss_AllHp = 20;//boss生命上限
    public float Boss_Hp = 20;//boss当前生命
    public float stick_speed = 20;
    public int Level = 1;//怪物的当前状态等级
}
