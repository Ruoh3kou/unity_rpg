using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Npc_dragon : Npc
{

    Npc_dragon()
    {
        DiaFileName = "dragon";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(dialog());
    }

    IEnumerator dialog()
    {
        yield return new WaitForSeconds(1.0f);

        //延迟1.0s触发对话
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
