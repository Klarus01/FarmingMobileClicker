using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List/Upgrade")]
public class UpgradeDataList : ScriptableObject
{
    [SerializeField] private List<UpgradeData> upgradeDataList;

    public List<UpgradeData> UpgradeDatasList { get { return upgradeDataList; } }
}
