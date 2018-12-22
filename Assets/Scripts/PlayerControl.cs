using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HedgehogTeam.EasyTouch;

public class PlayerControl : MonoBehaviour
{
    public bool IsDownMove = true;
    public bool IsUpMove = true;
    public bool IsLeftMove = true;
    public bool IsRightMove = true;
    private int hpcontrol = 5;
    public static bool restart_pushbox = false;
    // 攻击用
    public GameObject bullet;
    public float time = 1f;
    //拾取物品用
    public int Pickup_Judge;
    private int lastDir = 1;//控制角色方向 0上 1下 2左 3右

    private static Animator ani_body;// 身体动画
    // 动画状态
    enum State
    {
        idle_up,
        idle_down,
        idle_right,
        idle_left,
        move_up,
        move_down,
        move_right,
        move_left,

        attack_idle,
        attack_up,
        attack_down,
        attack_right,
        attack_left
    }
    State state = new State();

    // 初始化
    void Start()
    {
        ani_body = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Animator>();

        // 绑定按钮操作
        ObjectManager.Instance.button_B.onDown.AddListener(OpenBag);
        ObjectManager.Instance.button_J.onDown.AddListener(Attack);
        ObjectManager.Instance.button_K.onDown.AddListener(Mission_PushBox.Instance.doBoxMission);
        ObjectManager.Instance.button_K.onDown.AddListener(Dialog);
    }

    void Update()
    {
        if (time < 60)
            time += PlayerInfo.Instance.attackTime;

        Ani_Body_Update();

        if (PlayerInfo.Instance.Can_Move)
            Move();
        hurt();
        dead();
        if (PlayerInfo.Instance.Can_Move)
            ObjectManager.Instance.joystick.visible = true;
    }

    // 身体动画状态机
    void Ani_Body_Update()
    {
        switch (state)
        {
            case State.move_up:
                ani_body.SetBool("IsMove_Up", true);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            case State.move_down:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", true);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            case State.move_right:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", true);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            case State.move_left:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", true);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;

            case State.idle_up:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", true);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            case State.idle_down:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", true);
                break;
            case State.idle_left:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", false);
                ani_body.SetBool("IsIdle_Left", true);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            case State.idle_right:
                ani_body.SetBool("IsMove_Up", false);
                ani_body.SetBool("IsMove_Right", false);
                ani_body.SetBool("IsMove_Left", false);
                ani_body.SetBool("IsMove_Down", false);
                ani_body.SetBool("IsIdle_Up", false);
                ani_body.SetBool("IsIdle_Right", true);
                ani_body.SetBool("IsIdle_Left", false);
                ani_body.SetBool("IsIdle_Down", false);
                break;
            default:
                break;
        }
    }

    // 触发对话
    void Dialog()
    {
        if (PlayerInfo.Instance.Can_Dialogues)
        {
            DiaInfo.dialogue_panel.SetActive(true);
            PlayerInfo.Instance.Can_Move = false;// 对话不可攻击不可移动
            PlayerInfo.Instance.Can_Attack = false;
            PlayerInfo.Instance.Dia.DoDialogue();
            ObjectManager.Instance.joystick.visible = false;
        }
    }

    // 打开背包
    void OpenBag()
    {
        if (PlayerInfo.Instance.Bag_Open == false)
        {
            BagInfo.Instance.bag.SetActive(true);
            PlayerInfo.Instance.Bag_Open = true;
            PlayerInfo.Instance.Can_Move = false;
            PlayerInfo.Instance.Can_Attack = false;
        }
        else
        {
            BagInfo.Instance.bag.SetActive(false);
            PlayerInfo.Instance.Can_Move = true;
            PlayerInfo.Instance.Can_Attack = true;
            PlayerInfo.Instance.Bag_Open = false;
        }
    }
    void hurt()
    {

        for (int i = hpcontrol; i > PlayerInfo.Instance.Player_Hp - 1; i--)
        {
            ObjectManager.Instance.hp[i].gameObject.SetActive(false);
            hpcontrol--;
        }
    }
    void dead()
    {
        if (PlayerInfo.Instance.Player_Hp <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("角色死亡！");
            ObjectManager.Instance.DeathUI.SetActive(true);
        }
    }
    // 移动
    void Move()
    {
        // 摇杆
        float v = ETCInput.GetAxis("Vertical");
        float h = ETCInput.GetAxis("Horizontal");

        // 改变动画状态
        if (h > 0)
        {
            state = State.move_right;
            lastDir = 3;
        }
        else if (h < 0)
        {
            state = State.move_left;
            lastDir = 2;
        }
        else if (v > 0)
        {
            state = State.move_up;
            lastDir = 0;
        }
        else if (v < 0)
        {
            state = State.move_down;
            lastDir = 1;
        }
        else
        {
            switch (lastDir)
            {
                case 0:
                    state = State.idle_up;
                    break;
                case 1:
                    state = State.idle_down;
                    break;
                case 2:
                    state = State.idle_left;
                    break;
                case 3:
                    state = State.idle_right;
                    break;
                default:
                    break;
            }
        }

        if (v > 0.5)
            lastDir = 0;
        if (v < -0.5)
            lastDir = 1;
        if (h > 0.5)
            lastDir = 3;
        if (h < -0.5)
            lastDir = 2;


        // 移动
        if (v < 0 && IsDownMove)
        {
            this.transform.position += new Vector3(0, v * Time.deltaTime * PlayerInfo.Instance.Move_Speed, 0);
        }
        if (v > 0 && IsUpMove)
        {
            this.transform.position += new Vector3(0, v * Time.deltaTime * PlayerInfo.Instance.Move_Speed, 0);
        }
        if (h < 0 && IsLeftMove)
        {
            this.transform.position += new Vector3(h * Time.deltaTime * PlayerInfo.Instance.Move_Speed, 0, 0);
        }
        if (h > 0 && IsRightMove)
        {
            this.transform.position += new Vector3(h * Time.deltaTime * PlayerInfo.Instance.Move_Speed, 0, 0);
        }

    }

    // 攻击
    void Attack()
    {
        if (PlayerInfo.Instance.Can_Attack)
        {
            if (time >= 60)
            {
                time = 0;
                if (PlayerControl.ani_body.GetBool("IsMove_Up") == true || PlayerControl.ani_body.GetBool("IsIdle_Up") == true)
                {
                    time = 0;
                    bullet = GameObject.Instantiate(ObjectManager.player_bullet, this.transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

                }
                if (PlayerControl.ani_body.GetBool("IsMove_Down") == true || PlayerControl.ani_body.GetBool("IsIdle_Down") == true)
                {
                    time = 0;
                    bullet = GameObject.Instantiate(ObjectManager.player_bullet, this.transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0);

                }
                if (PlayerControl.ani_body.GetBool("IsMove_Left") == true || PlayerControl.ani_body.GetBool("IsIdle_Left") == true)
                {
                    time = 0;
                    bullet = GameObject.Instantiate(ObjectManager.player_bullet, this.transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90f);

                }
                if (PlayerControl.ani_body.GetBool("IsMove_Right") == true || PlayerControl.ani_body.GetBool("IsIdle_Right") == true)
                {
                    time = 0;
                    bullet = GameObject.Instantiate(ObjectManager.player_bullet, this.transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
                }
            }
            else
            {
                time += PlayerInfo.Instance.attackTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 任务
        if (other.tag == "push_begin")
        {
            Debug.Log("进行宝箱任务");
            Mission_PushBox.Instance.Can_start = true;
            PlayerInfo.Instance.Can_Attack = false;
        }
        if (other.tag == "Restart_pushbox")
        {
            Mission_PushBox.Instance.Can_restart = true;
            PlayerInfo.Instance.Can_Attack = false;
        }
        // 滑动冰块
        if (other.tag == "SlideBox")
        {
            Debug.Log("Slide to " + lastDir);
            switch (lastDir)
            {
                case 0:
                    iTween.MoveTo(this.gameObject, iTween.Hash("y", this.transform.position.y + 20, "easeType", "easeOutQuad", "time", 4f));
                    break;
                case 1:
                    iTween.MoveTo(this.gameObject, iTween.Hash("y", this.transform.position.y - 20, "easeType", "easeOutQuad", "time", 4f));
                    break;
                case 2:
                    iTween.MoveTo(this.gameObject, iTween.Hash("x", this.transform.position.x - 20, "easeType", "easeOutQuad", "time", 4f));
                    break;
                case 3:
                    iTween.MoveTo(this.gameObject, iTween.Hash("x", this.transform.position.x + 20, "easeType", "easeOutQuad", "time", 4f));
                    break;
                default:
                    break;
            }
        }
        //捡起兽皮
        if (other.tag == "AnimalSkin")
        {
            Pickup_Judge = Pickup(other, 1);
            if (Pickup_Judge == 1)
            {
                GameObject.Destroy(other.gameObject);
            }
        }
        //捡起小花
        if (other.tag == "smallflower")
        {
            Pickup_Judge = Pickup(other, 2);
            if (Pickup_Judge == 1)
            {
                GameObject.Destroy(other.gameObject);
            }
        }
        if (other.tag == "chicken_stick")
        {
            PlayerInfo.Instance.Player_Hp--;
            Destroy(other.gameObject);
        }
        if (other.tag == "boss")
        {
            PlayerInfo.Instance.Player_Hp--;
        }
        if (other.tag == "Restart_pushbox")
        {
            restart_pushbox = true;
        }
    }
    //拾取物品
    public int Pickup(Collider2D other, int ID)
    {
        if (BagInfo.Instance.Itemlist[13] == 0)//背包没满
        {
            for (int i = 0; i < 13; i++)
            {
                if (BagInfo.Instance.Itemlist[i] == 0)//背包中没有这个物品
                {
                    BagInfo.Instance.Itemlist[i] = ID;
                    BagInfo.Instance.ItemNum[i] += 1;
                    return 1;
                }
                else if (BagInfo.Instance.Itemlist[i] == ID)//背包中有这个物品
                {
                    BagInfo.Instance.ItemNum[i]++;
                    return 1;
                }
            }
            return 1;
        }
        else
            return 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 任务
        if (other.tag == "push_begin")
        {
            Debug.Log("离开宝箱");
            Mission_PushBox.Instance.Can_start = false;
            PlayerInfo.Instance.Can_Attack = true;
        }
        if (other.tag == "Restart_pushbox")
        {
            Mission_PushBox.Instance.Can_restart = false;
            PlayerInfo.Instance.Can_Attack = true;
        }
    }

}
