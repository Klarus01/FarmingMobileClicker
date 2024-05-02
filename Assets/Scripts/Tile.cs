using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject redHighlight;

    public bool halfObjectInside = false;
    public bool isOccupied = false;

    public void Init(bool isOffset)
    {
        renderer.color = isOffset ? offsetColor : baseColor;
    }

    private void Update()
    {
        if (!halfObjectInside)
        {
            DeactivateRedHighlight();
            DeactivateHighlight();
            return;
        }

        if (isOccupied) ActivateRedHighlight();
        else ActivateHighlight();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out BuildingPlacingHandler building)) return;

        halfObjectInside = IsHalfObjectInside(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        halfObjectInside = false;
    }

    private bool IsHalfObjectInside(Collider2D other)
    {
        Bounds triggerBounds = other.bounds;
        Bounds objectBounds = GetComponent<Collider2D>().bounds;

        float horizontalOverlap = Mathf.Min(triggerBounds.max.x, objectBounds.max.x) - Mathf.Max(triggerBounds.min.x, objectBounds.min.x);
        float verticalOverlap = Mathf.Min(triggerBounds.max.y, objectBounds.max.y) - Mathf.Max(triggerBounds.min.y, objectBounds.min.y);

        if (horizontalOverlap >= objectBounds.size.x / 2 && verticalOverlap >= objectBounds.size.y / 2)
        {
            return true;
        }

        return false;
    }

    public void ActivateHighlight()
    {
        highlight.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        highlight.SetActive(false);
    }

    public void ActivateRedHighlight()
    {
        redHighlight.SetActive(true);
    }

    public void DeactivateRedHighlight()
    {
        redHighlight.SetActive(false);
    }
}