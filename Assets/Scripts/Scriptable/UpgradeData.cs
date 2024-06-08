using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType type;
    public float value;

    public void ApplyUpgrade()
    {
        UpgradeHandler.Instance.AddUpgrade(type, value);
    }
}