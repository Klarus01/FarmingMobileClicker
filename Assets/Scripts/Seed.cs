using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private SoilClickHandler clickHandler;
    [SerializeField] private SeedsData seed;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    private Soil soil;

    private void Start()
    {
        image.sprite = seed.ItemSprite;
        button.onClick.AddListener(PlantSeedInSoil);
    }

    public void SetSoil(Soil soil)
    {
        this.soil = soil;
    }

    private void PlantSeedInSoil()
    {
        soil.PlantSeed(seed);
        clickHandler.SeedsPanel.SetActive(false);
    }
}