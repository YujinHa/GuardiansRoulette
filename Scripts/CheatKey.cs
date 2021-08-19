using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatKey : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public static void SetCheatKey()
    {
        PlayerState.HP = 6f;
        PlayerState.Coin = 30;
        if (PlayerState.goal == 0)
        {
            PlayerState.CurrentQuest = 8;
            PlayerState.Exp = 97f;

            PlayerState.GotSkill[0] = true;
        }
        else if (PlayerState.goal == 1)
        {
            PlayerState.CurrentQuest = 18;
            PlayerState.ExpLv = 1;
            PlayerState.Exp = 130f;

            PlayerState.GotSkill[0] = true;
            PlayerState.GotSkill[1] = true;
            PlayerState.GotSkill[6] = true;
        }
        else if (PlayerState.goal == 2)
        {
            PlayerState.CurrentQuest = 28;
            PlayerState.ExpLv = 1;
            PlayerState.Exp = 185f;

            for (int i = 0; i < 9; i++)
            {
                PlayerState.GotSkill[i] = true;
            }
        }
        ItemState.ItemCount[0] = 2;
        ItemState.ItemCount[3] = 1;
        ItemState.ItemCount[6] = 2;
        ItemState.ItemCount[7] = 1;
        ItemState.ItemCount[8] = 3;
        ItemState.ItemCount[9] = 4;
        ItemState.ItemCount[10] = 2;
        ItemState.ItemCount[12] = 2;
        ItemState.ItemCount[13] = 3;
        ItemState.ItemCount[15] = 2;

        PetState.AbleTotalPetNum = 2;
        PetState.PetNum = 2;
        PetState.GotPetNum = 3;
        PetState.PetStates[0] = 0;
        PetState.PetStates[1] = 2;
        PetState.PetStates[2] = 2;
        PetState.PetStates[3] = 0;
        PetState.PetStates[4] = 1;
        PetState.Health[4] = 10;
        PetState.Health[2] = 1;
        PetState.Health[1] = 8;
        PetState.Health[0] = 10;
        PetState.Health[3] = 10;
        PetState.Exp[2] = 4;
        PetState.Exp[1] = 2;
        PetState.Exp[0] = 0;
        PetState.Exp[3] = 0;
        PetState.Exp[4] = 0;
        PetState.Lv[2] = 1;
        PetState.Lv[1] = 2;
        PetState.Lv[0] = 0;
        PetState.Lv[3] = 0;
        PetState.Lv[4] = 0;
    }
}
