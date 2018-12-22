using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {
    public float watchRange = 3.0f;//监视范围
    public float attRange = 0.5f;//攻击范围
    public float attackTime = 0;//记录攻击间隔
    public float Monster_Move_Speed = 2;//怪物行走速度
    public float Monster_Catch_Speed = 2;//怪物追击速度
    public float Monster_AllHp = 4;//怪物的总血量上限
    public float Monster_Hp = 4;//怪物的当前血量
    public int Monster_Attack_num = 1;//怪物的攻击力
}
