using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Egg :Npc {
    Npc_Egg()
    {
        DiaFileName = "player1";
    }

    private void Update()
    {
        for (int i = 0; i < 13; i++)
        {
            if (BagInfo.Instance.Itemlist[i] == 2)//背包中有花
            {
                DiaFileName = "player2";
            }
        }
    }
}
