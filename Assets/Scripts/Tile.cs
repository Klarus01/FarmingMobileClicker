using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject redHighlight;

    public bool isOccupied = false;

    public void Init(bool isOffset)
    {
        renderer.color = isOffset ? offsetColor : baseColor;
    }

    /*private void OnMouseEnter()
    {
        ActivateHighlight();
    }

    private void OnMouseExit()
    {
        DeactivateHighlight();
    }*/

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