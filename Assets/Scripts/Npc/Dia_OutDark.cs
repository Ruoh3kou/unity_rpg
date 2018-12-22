using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dia_OutDark : Npc
{
    Dia_OutDark()
    {
        DiaFileName = "outdark";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectManager.Instance.Dark_Mesh.SetActive(false);
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
