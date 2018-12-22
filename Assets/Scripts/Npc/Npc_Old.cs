using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Old : Npc
{
    Npc_Old()
    {
        DiaFileName = "old1";
    }

    private void Update()
    {
        for (int i = 0; i < 13; i++)
        {
            if (BagInfo.Instance.Itemlist[i] == 1)//背包中有兽皮
            {
                DiaFileName = "old2";
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo.Instance.Can_Attack = false;
            PlayerInfo.Instance.Can_Dialogues = true;
            PlayerInfo.Instance.Dia = new Dialogue(DiaFileName);
            Debug.Log("进入对话区域-" + DiaFileName);
            if (DiaFileName == "old2")
            {
                for (int i = 0; i < 13; i++)
                {
                    if (BagInfo.Instance.Itemlist[i] == 1)//背包中有兽皮
                    {
                        BagInfo.Instance.Itemlist[i] = 3;
                    }
                }
            }
        }
    }
}
