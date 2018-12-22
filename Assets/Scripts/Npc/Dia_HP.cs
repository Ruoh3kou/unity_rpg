using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dia_HP : Npc
{
    Dia_HP()
    {
        DiaFileName = "hp";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = PlayerInfo.Instance.Player_Hp; i < 6; i++)
        {
            ObjectManager.Instance.hp[i].gameObject.SetActive(true);
        }
        PlayerInfo.Instance.Player_Hp = 6;
        StartCoroutine(dialog());
    }

    IEnumerator dialog()
    {
        yield return new WaitForSeconds(0.5f);
        {
            PlayerInfo.Instance.Can_Dialogues = true;
            DiaInfo.dialogue_panel.SetActive(true);
            PlayerInfo.Instance.Can_Move = false;
            PlayerInfo.Instance.Can_Attack = false;
            PlayerInfo.Instance.Dia = new Dialogue(DiaFileName);
            PlayerInfo.Instance.Dia.DoDialogue();
            ObjectManager.Instance.joystick.visible = false;
        }
    }
}
