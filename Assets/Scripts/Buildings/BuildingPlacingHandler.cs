using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPlacingHandler : MonoBehaviour
{
    [SerializeField] private int buildingSize;
    [SerializeField] private ClickableObject clickableObject;
    private Color baseColor;
    private new SpriteRenderer renderer;
    private List<Collider2D> colliders = new();
    private FarmerAI farmer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        baseColor = renderer.color;
        baseColor.a = 0.75f;
        renderer.color = baseColor;
    }

    private void Update()
    {
        MoveBuilding();

        if (Input.GetMouseButtonDown(0) && colliders.Count.Equals(buildingSize))
        {
            PlaceBuilding(CenterPosition());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Tile>(out var tile)) return;
        if (tile.isOccupied) return;
        if (!tile.halfObjectInside)
        {
            if (colliders.Contains(collision)) colliders.Remove(collision);
            return;
        }

        if (!colliders.Contains(collision)) colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Tile>()) return;
        
        if (colliders.Contains(collision)) colliders.Remove(collision);
    }

    private void MoveBuilding()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private Vector3 CenterPosition()
    {
        Vector3 center = Vector3.zero;

        foreach (var collider in colliders)
        {
            center += collider.bounds.center;
            collider.GetComponent<Tile>().isOccupied = true;
            collider.GetComponent<Tile>().halfObjectInside = false;
            collider.GetComponent<Tile>().DeactivateHighlight();
        }

        center /= colliders.Count;

        return center;
    }

    private void PlaceBuilding(Vector3 newPos)
    {
        transform.position = newPos;
        baseColor.a = 1f;
        renderer.color = baseColor;
        if (clickableObject)
        {
            clickableObject.enabled = true;
        }
        
        farmer = FindObjectOfType<FarmerAI>();
        if (farmer)
        {
            if (gameObject.GetComponent<Soil>())
            {
                farmer.AddSoil(gameObject.GetComponent<Soil>());
            }
        }
        
        Destroy(this);
    }
}