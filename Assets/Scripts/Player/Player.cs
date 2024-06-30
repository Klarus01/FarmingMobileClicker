using UnityEngine;
using System;
using Unity.VisualScripting;

public class Player : SingletoneMonobehaviour<Player>
{
    public Action OnLevelUp;
    public Action OnGrainExp;

    [SerializeField] private UpgradeDataList upgradeDataList;

    private int level = 1;

    private int money = 100;

    private int expToLevelUp = 50;
    [SerializeField, Range(0, 1000)] private int exp;
    private int upgradePointsToSpend = 0;
    public int UpgradePointsToSpend { get { return upgradePointsToSpend; } set { upgradePointsToSpend = value; } }
    
    private Inventory inventory;

    private readonly int EXP_TO_LEVEL_UP_MULTIPLIER = 2;

    public int Exp { get { return exp; } }
    public int Level { get { return level; } }
    public int ExpToLevelUp { get { return expToLevelUp; } }
    public int Money { get { return money; } }

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
        if (money + moneyToAdd >= 0)
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
        upgradePointsToSpend++;
        GrainUpgrade();
        OnLevelUp?.Invoke();
        TryToLevelUp();
    }

    private void GrainUpgrade()
    {
        int upgradeIndex = UnityEngine.Random.Range(0, upgradeDataList.UpgradeDatasList.Count);

        UpgradeHandler.Instance.AddUpgrade(upgradeDataList.UpgradeDatasList[upgradeIndex]);
    }
}
