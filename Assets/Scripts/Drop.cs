using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    MonsterInfo monInfo;
    GameObject player;
    private Animator ani_body;// 身体动画
    private bool is_drop = false;
    private GameObject dropthing;
    public Vector3[] patrolPaths = new Vector3[4];
    enum State
    {
        state_patrol,
        state_hide,
        state_attack,
        state_die
    }
    State curState = new State();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monInfo = this.GetComponent<MonsterInfo>();
        ani_body = this.GetComponent<Animator>();
        dropthing = (GameObject)Resources.Load("Prefabs/shoupi (1)_0");
        patrolPaths[0] = transform.position + new Vector3(5, 5, 0);
        patrolPaths[1] = transform.position + new Vector3(0, 5, 0);
        patrolPaths[2] = transform.position + new Vector3(-5, 5, 0);
        patrolPaths[3] = transform.position + new Vector3(-5, 0, 0);
    }
    void Update()
    {
        StateControl();
        StateUpate();
    }
    void StateUpate()
    {
        switch (curState)
        {
            case State.state_attack:
                ani_body.SetBool("Is_Attack", true);
                ani_body.SetBool("Is_Move", false);
                ani_body.SetBool("Is_Die", false);
                Mon_Attack();
                break;
            case State.state_die:
                ani_body.SetBool("Is_Attack", false);
                ani_body.SetBool("Is_Move", false);
                ani_body.SetBool("Is_Die", true);
                Mon_Die();
                break;
            case State.state_hide:
                ani_body.SetBool("Is_Attack", false);
                ani_body.SetBool("Is_Move", true);
                ani_body.SetBool("Is_Die", false);
                Mon_Hide();
                break;
            case State.state_patrol:
                ani_body.SetBool("Is_Attack", false);
                ani_body.SetBool("Is_Move", true);
                ani_body.SetBool("Is_Die", false);
                Mon_Patrol();
                break;
            default:
                break;
        }
    }

    // 模糊状态逻辑控制
    void StateControl()
    {
        // 主角剩余血量
        float RplayerHp = PlayerInfo.Instance.Player_Hp / PlayerInfo.Instance.Player_AllHp;
        // 怪兽剩余血量比
        float RmonsterHp = monInfo.Monster_Hp / monInfo.Monster_AllHp;

        // 死亡
        if (monInfo.Monster_Hp <= 0)
            curState = State.state_die;
        else
        {
            // 攻击/闪避
            if (IsInRange())
            {
                // 怪物血量健康 攻击
                if (RmonsterHp > 0.5)
                    curState = State.state_attack;
                else
                    curState = State.state_hide;
                // 主角血量危险 攻击
                if (RplayerHp < 0.1)
                    curState = State.state_attack;
            }
            // 巡逻
            else
            {
                curState = State.state_patrol;
            }
        }

    }
    // 进入攻击范围
    bool IsInRange()
    {
        float dis = (player.transform.position - this.transform.position).magnitude;
        if (dis <= monInfo.attRange)
        {
            return true;
        }
        return false;
    }
    // 怪物攻击
    void Mon_Attack()
    {
        float dis = (player.transform.position - this.transform.position).magnitude;
        Vector3 dir = player.transform.position - this.transform.position;
        if (dis > 1.5)
        {
            iTween.Stop(this.gameObject);
            GetComponent<Rigidbody2D>().MovePosition(this.transform.position + dir.normalized * monInfo.Monster_Catch_Speed * Time.deltaTime);
        }
    }
    // 怪物巡逻
    void Mon_Patrol()
    {
        Hashtable args = new Hashtable();
        //设置路径的点
        args.Add("path", patrolPaths);
        //设置类型为线性，线性效果会好一些。
        args.Add("easeType", iTween.EaseType.linear);
        //设置寻路的速度
        args.Add("speed", monInfo.Monster_Move_Speed);
        //是否先从原始位置走到路径中第一个点的位置
        args.Add("movetopath", true);

        //让模型开始寻路	
        iTween.MoveTo(gameObject, args);
    }
    // 怪物躲避
    void Mon_Hide()
    {
        float dis = (player.transform.position - this.transform.position).magnitude;
        Vector3 dir = this.transform.position - player.transform.position;
        if (dis <= 3)
        {
            iTween.Stop(this.gameObject);
            GetComponent<Rigidbody2D>().MovePosition(this.transform.position + dir.normalized * monInfo.Monster_Catch_Speed * Time.deltaTime);
        }
    }
    //怪物掉落物品
    void drop()
    {
        if (is_drop == false)
        {
            GameObject.Instantiate(dropthing, new Vector3(this.transform.position.x - 1, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
            is_drop = true;
        }
    }
    // 怪物死亡
    void Mon_Die()
    {
        iTween.Stop(this.gameObject);
        Destroy(this.gameObject, 1.5f);
        drop();
    }

}
