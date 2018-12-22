using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour
{
    public float bullet_speed = 5.0f;
    private GameObject player;
    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.Translate(Vector2.down * bullet_speed * Time.deltaTime);
        if (this.transform.position.x < (player.transform.position.x - PlayerInfo.Instance.attackdistance) || this.transform.position.x > (player.transform.position.x + PlayerInfo.Instance.attackdistance) || this.transform.position.y < (player.transform.position.y - PlayerInfo.Instance.attackdistance) || this.transform.position.y > (player.transform.position.y + PlayerInfo.Instance.attackdistance))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "monster")
        {
            collision.GetComponent<MonsterInfo>().Monster_Hp -= PlayerInfo.Instance.Player_Attack_num;
            Debug.Log(collision.GetComponent<MonsterInfo>().Monster_Hp);
            GameObject.Destroy(this.gameObject);
        }

        if (collision.tag == "boss")
        {
            collision.GetComponent<BossInfo>().Boss_Hp -= PlayerInfo.Instance.Player_Attack_num;
            Debug.Log(collision.GetComponent<BossInfo>().Boss_Hp);
            GameObject.Destroy(this.gameObject);
        }
    }
}
