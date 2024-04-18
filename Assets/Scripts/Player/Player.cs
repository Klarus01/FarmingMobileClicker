using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int level = 1;

    private int money;

    private int exp;
    private int expToLevelUp;

    private readonly int EXP_TO_LEVEL_UP_MULTIPLIER = 2;

    public void AddExp(int expToAdd)
    {
        exp += expToAdd;
        TryToLevelUp();
    }

    private void TryToLevelUp()
    {
        if(exp != expToLevelUp)
        {
            return;
        }

        level++;
        exp -= expToLevelUp;
        expToLevelUp *= EXP_TO_LEVEL_UP_MULTIPLIER;

        TryToLevelUp();
    }
}
