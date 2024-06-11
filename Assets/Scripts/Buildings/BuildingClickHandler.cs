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
}