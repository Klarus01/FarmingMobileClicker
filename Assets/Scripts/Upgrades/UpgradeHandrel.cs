using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandrel : SingletoneMonobehaviour<UpgradeHandrel>
{
    [SerializeField] private Dictionary<UpgradeData, int> countByUpgradeData = new(); 

    public Dictionary<UpgradeData, int> CountByUpgradeData { get { return countByUpgradeData; } }

    public void AddUpgrade(UpgradeData upgradeData, int count = 1)
    {
        if(countByUpgradeData.ContainsKey(upgradeData))
        {
            countByUpgradeData[upgradeData] += count;
            return;
        }

        countByUpgradeData.Add(upgradeData, count);
    }
}
