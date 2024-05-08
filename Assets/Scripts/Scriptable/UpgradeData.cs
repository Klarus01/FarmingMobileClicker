using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType type;
    public float value;

    public void ApplyUpgrade()
    {
        UpgradeHandrel.Instance.AddUpgrade(type, value);
    }
}