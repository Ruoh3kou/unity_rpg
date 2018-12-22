using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    protected string DiaFileName;
    public Npc()
    {
        DiaFileName = "";
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo.Instance.Can_Attack = false;
            PlayerInfo.Instance.Can_Dialogues = true;
            PlayerInfo.Instance.Dia = new Dialogue(DiaFileName);
            Debug.Log("进入对话区域-" + DiaFileName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo.Instance.Can_Dialogues = false;
            PlayerInfo.Instance.Can_Attack = true;
            Debug.Log("离开对话区域-" + DiaFileName);
        }
    }
}
