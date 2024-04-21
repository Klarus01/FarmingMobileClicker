using UnityEngine;

public class FieldClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject seedsPanel;
    private int seedsPanelWidth = 1;
    private int seedsUnlocked = 2;

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            CheckFieldsUnderMouse();
        }
    }

    private void CheckFieldsUnderMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent<Tile>(out var tile))
        {
            SeedsPanelHandler(tile);
            return;
        }

        seedsPanel.SetActive(false);
    }

    private void SeedsPanelHandler(Tile tile)
    {
        PanelWidth();

        if (!seedsPanel.activeSelf)
        {
            seedsPanel.SetActive(true);
        }

        seedsPanel.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 1f);
    }

    private void PanelWidth()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = seedsPanelWidth * seedsUnlocked;
        seedsPanel.transform.localScale = newScale;
    }
}