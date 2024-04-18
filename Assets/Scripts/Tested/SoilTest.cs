using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilTest : MonoBehaviour
{
    public Seeds seeds;

    private void Start()
    {
        Soil soil = GetComponent<Soil>();
        soil.PlantSeed(seeds);
    }
}
