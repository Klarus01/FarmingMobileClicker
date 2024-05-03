using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Action OnLevelUp;
    public Action OnGrainExp;

    private int level = 1;

    private int money;

    private int exp;
    private int expToLevelUp = 50;

    private readonly int EXP_TO_LEVEL_UP_MULTIPLIER = 2;

    public int Exp { get { return exp; } }
    public int Level { get { return level; } }
    public int ExpToLevelUp { get { return expToLevelUp; } }

    private void Start()
    {
        OnGrainExp?.Invoke();
    }

    public void AddExp(int expToAdd)
    {
        exp += expToAdd;
        TryToLevelUp();

        OnGrainExp?.Invoke();
    }

    private void TryToLevelUp()
    {
        if (exp < expToLevelUp) return;

        level++;
        exp -= expToLevelUp;
        expToLevelUp *= EXP_TO_LEVEL_UP_MULTIPLIER;
        OnLevelUp?.Invoke();
        TryToLevelUp();
    }
}
