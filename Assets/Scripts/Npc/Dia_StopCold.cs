using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dia_StopCold : Npc
{

    Dia_StopCold()
    {
        DiaFileName = "stopcold";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PlayerInfo.Instance.GetCloth)
            StartCoroutine(dialog());
        else
            this.gameObject.SetActive(false);
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
            PlayerInfo.Instance.Dia = new Dialogue(DiaFileName);
            PlayerInfo.Instance.Dia.DoDialogue();
            ObjectManager.Instance.joystick.visible = false;
        }
    }

}
