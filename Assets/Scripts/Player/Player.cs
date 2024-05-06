using UnityEngine;
using System;

public class Player : SingletoneMonobehaviour<Player>
{
    public Action OnLevelUp;
    public Action OnGrainExp;

    private int level = 1;

    private int money = 100;

    private int exp;
    private int expToLevelUp = 50;

    private float cropYield = 1f;
    private float growthSpeed = 1f;
    private float cropPrice = 1f;
    
    private Inventory inventory;

    private readonly int EXP_TO_LEVEL_UP_MULTIPLIER = 2;

    public int Exp { get { return exp; } }
    public int Level { get { return level; } }
    public int ExpToLevelUp { get { return expToLevelUp; } }
    public int Money { get { return money; } }
    
    public float CropYield { get => cropYield; set => cropYield = value; }
    public float GrowthSpeed { get => growthSpeed; set => growthSpeed = value; }
    public float CropPrice { get => cropPrice; set => cropPrice = value; }

    public Inventory Inventory { get { return inventory; } }

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

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

    public bool UpdateMoney(int moneyToAdd)
    {
        if (money + moneyToAdd > 0)
        {
            money += moneyToAdd;
            return true;
        }
        Debug.Log("nie masz hajsu biedaku");
        return false;
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
