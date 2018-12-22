using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    GameObject player;
    GameObject chicken_stick;
    private Animator ani_body;//身体动画
    enum State
    {
        state_idle,
        state_attack,
        state_catch,
        state_die,
    }
    State curState = new State();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chicken_stick = (GameObject)Resources.Load("Prefabs/chicken_stick");

        ani_body = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossInfo.Instance.attackTime <= 70)
            ++BossInfo.Instance.attackTime;
        StateUpdate();
    }
    private void FixedUpdate()
    {
        StateControl();
    }
    void StateControl()
    {
        if (BossInfo.Instance.Boss_Hp <= 0)
            curState = State.state_die;
        else
        {
            if (IsInRange())
            {
                curState = State.state_attack;
                BossInfo.Instance.watchRange = 9999;
            }
            else
            {
                curState = State.state_idle;
            }
        }
    }
    void StateUpdate()
    {
        switch (curState)
        {
            case State.state_attack:
                ani_body.SetBool("is_attack", true);
                ani_body.SetBool("is_walk", false);
                ani_body.SetBool("is_die", false);
                ani_body.SetBool("is_stand", false);
                boss_attack();
                break;
            case State.state_catch:
                ani_body.SetBool("is_attack", false);
                ani_body.SetBool("is_walk", true);
                ani_body.SetBool("is_die", false);
                ani_body.SetBool("is_stand", false);
                break;
            case State.state_die:
                ani_body.SetBool("is_attack", false);
                ani_body.SetBool("is_walk", false);
                ani_body.SetBool("is_die", true);
                ani_body.SetBool("is_stand", false);
                boss_die();
                break;
            case State.state_idle:
                ani_body.SetBool("is_attack", false);
                ani_body.SetBool("is_walk", false);
                ani_body.SetBool("is_die", false);
                ani_body.SetBool("is_stand", true);
                break;
            default:
                break;
        }
    }
    //进入攻击范围
    bool IsInRange()
    {
        float dis = (player.transform.position - this.transform.position).magnitude;
        if (dis <= BossInfo.Instance.watchRange)
        {
            return true;
        }
        return false;
    }
    //boss攻击
    void boss_attack()//continue to do....
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        float dis = (player.transform.position - this.transform.position).magnitude;
        Vector3 dir = player.transform.position - this.transform.position;
        /*if (dis <= BossInfo.Instance.attRange && PlayerInfo.Instance.Player_Hp > 0 && BossInfo.Instance.attackTime >= 70)
        {
            PlayerInfo.Instance.Player_Hp -= BossInfo.Instance.Boss_Attack_num;
            Debug.Log("Boss hit playerHP:" + PlayerInfo.Instance.Player_Hp);
            BossInfo.Instance.attackTime = 0;
        }*/
        if (BossInfo.Instance.attackTime >= 70)
        {
            attck_level_1();
            BossInfo.Instance.attackTime = 0;
        }
        if (dis > 0.5)
        {
            iTween.Stop(this.gameObject);
            GetComponent<Rigidbody2D>().MovePosition(this.transform.position + dir.normalized * BossInfo.Instance.Boss_Catch_Speed * Time.deltaTime);
        }
    }
    void boss_die()
    {
        iTween.Stop(this.gameObject);
        StartCoroutine(dialog());
        ObjectManager.Instance.photo.SetActive(true);
        ObjectManager.Instance.mainCamera.GetComponent<AudioSource>().Pause();
        Destroy(this.gameObject, 1.5f);
    }
    IEnumerator dialog()
    {
        yield return new WaitForSeconds(0.5f);
        //延迟0.5s触发对话
        {
            PlayerInfo.Instance.Can_Dialogues = true;
            DiaInfo.dialogue_panel.SetActive(true);
            PlayerInfo.Instance.Can_Move = false;
            PlayerInfo.Instance.Can_Attack = false;
            PlayerInfo.Instance.Dia = new Dialogue("over1");
            PlayerInfo.Instance.Dia.DoDialogue();
            ObjectManager.Instance.joystick.visible = false;
        }
    }
    void attck_level_1()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        GameObject bullet1 = GameObject.Instantiate(chicken_stick, this.transform.position, Quaternion.identity);
        bullet1.GetComponent<Rigidbody2D>().AddForce(dir * 40);
        Destroy(bullet1.gameObject, 5);
    }
}
