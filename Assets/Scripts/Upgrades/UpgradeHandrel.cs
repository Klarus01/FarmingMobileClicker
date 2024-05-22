using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandrel : SingletoneMonobehaviour<UpgradeHandrel>
{
    [SerializeField] private Dictionary<UpgradeType, float> valueByUpgradeType = new(); 

    public Dictionary<UpgradeType, float> ValueByUpgradeType { get { return valueByUpgradeType; } }

    public void AddUpgrade(UpgradeType upgradeType, float value)
    {
        if(valueByUpgradeType.ContainsKey(upgradeType))
        {
            valueByUpgradeType[upgradeType] += value;
            return;
        }

        valueByUpgradeType.Add(upgradeType, value);
    }

    internal void AddUpgrade(UpgradeData upgradeData)
    {
        AddUpgrade(upgradeData.type, upgradeData.value);
    }
}
