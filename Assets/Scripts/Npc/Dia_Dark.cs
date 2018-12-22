using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dia_Dark : Npc
{
    Dia_Dark()
    {
        DiaFileName = "dark";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectManager.Instance.Dark.SetActive(true);
        ObjectManager.Instance.Dark_Mesh.SetActive(true);
        dialog();
        DiaFileName = "dark2";
    }
    private void Update()
    {
        if (PlayerInfo.Instance.GetLight)
            StartCoroutine(dialog2());
    }
    void dialog()
    {
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

    IEnumerator dialog2()
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
