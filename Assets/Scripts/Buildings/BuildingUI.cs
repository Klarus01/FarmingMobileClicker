using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private Building Building;
    [SerializeField] private TMP_Text MessageText;

    public void OnProduceButtonClick()
    {
        if (Building.CanProduce(Player.Instance.Inventory))
        {
            Building.Produce(Player.Instance.Inventory);
            MessageText.SetText("Start production.");
        }
        else
        {
            MessageText.SetText("Not enough crops!");
        }
    }
}