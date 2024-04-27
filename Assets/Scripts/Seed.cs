using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private SeedsData seed;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    public Soil soil;

    private void Start()
    {
        image.sprite = seed.ItemSprite;
        button.onClick.AddListener(PlantSeedInSoil);
    }

    private void PlantSeedInSoil()
    {
        soil.PlantSeed(seed);
    }
}
