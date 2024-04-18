using UnityEngine;

public class BuildingPlacingHandler : MonoBehaviour
{
    private Color baseColor;
    private SpriteRenderer renderer;
    private Vector3 pos;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        baseColor = renderer.color;
        baseColor.a = 0.75f;
        renderer.color = baseColor;
    }

    private void Update()
    {
        pos = Input.mousePosition;
        pos.z = 100;
        transform.position = Camera.main.ScreenToWorldPoint(pos);

        if (Input.GetMouseButtonDown(0))
        {
            CheckIfPlaceIsOccupied();
        }
    }

    private void CheckIfPlaceIsOccupied()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent<Tile>(out var tile))
        {
            if (!tile.isOccupied)
            {
                tile.isOccupied = true;
                PlaceBuilding(tile);
                return;
            }
        }
    }

    private void PlaceBuilding(Tile tile)
    {
        transform.position = tile.transform.position;
        baseColor.a = 1f;
        renderer.color = baseColor;
        gameObject.GetComponent<BuildingPlacingHandler>().enabled = false;
    }
}