using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private Building building;

    private void OnMouseDown()
    {
        ShowActionPanel();
    }

    private void ShowActionPanel()
    {
        actionPanel.SetActive(true);
    }

    public void OnProduceButtonClick()
    {
        BuildingUI buildingUI = actionPanel.GetComponent<BuildingUI>();
        if (buildingUI != null)
        {
            buildingUI.OnProduceButtonClick();
        }
    }

    public void OnCancelButtonClick()
    {
        actionPanel.SetActive(false);
    }
}