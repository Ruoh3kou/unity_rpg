using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public void final1()
    {
        ObjectManager.Instance.bt1.SetActive(false);
        dialog("final1");
    }
    public void final2()
    {
        ObjectManager.Instance.bt2.SetActive(false);
        dialog("final2");
    }
    public void final3()
    {
        ObjectManager.Instance.bt3.SetActive(false);
        ObjectManager.Instance.bt4.SetActive(false);

        dialog("final3");
    }
    public void final4()
    {
        ObjectManager.Instance.bt3.SetActive(false);
        ObjectManager.Instance.bt4.SetActive(false);
        dialog("final4");
    }
    public void final5()
    {
        ObjectManager.Instance.bt5.SetActive(false);
        dialog("final5");
    }

    void dialog(string str)
    {
        PlayerInfo.Instance.Can_Dialogues = true;
        DiaInfo.dialogue_panel.SetActive(true);
        PlayerInfo.Instance.Can_Move = false;
        PlayerInfo.Instance.Can_Attack = false;
        PlayerInfo.Instance.Dia = new Dialogue(str);
        PlayerInfo.Instance.Dia.DoDialogue();
        ObjectManager.Instance.joystick.visible = false;
    }
}
