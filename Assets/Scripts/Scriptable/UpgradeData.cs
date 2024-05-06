using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public enum UpgradeType { CropYield, GrowthSpeed, CropPrice }

    public string upgradeName;
    public UpgradeType type;
    public float value;

    public void ApplyUpgrade()
    {
        switch (type)
        {
            case UpgradeType.CropYield:
                Player.Instance.CropYield += value;
                break;
            case UpgradeType.GrowthSpeed:
                Player.Instance.GrowthSpeed += value;
                break;
            case UpgradeType.CropPrice:
                Player.Instance.CropPrice += value;
                break;
            default:
                Debug.LogError("Unknown upgrade type!");
                break;
        }
    }
}