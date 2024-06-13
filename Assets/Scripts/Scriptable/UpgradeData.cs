using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType type;
    public float value;
    public Color color
    {
        get
        {
            if(type == UpgradeType.GrowthSpeed)
            {
                return Color.blue;
            }

            if(type == UpgradeType.CropYield)
            {
                return Color.yellow;
            }

            if(type == UpgradeType.CropPrice)
            {
                return Color.green;
            }

            return Color.white;
        }
    }

    public void ApplyUpgrade()
    {
        UpgradeHandler.Instance.AddUpgrade(type, value);
    }
}