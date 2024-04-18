using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Building/ProcessingPlants")]
public class ProcessingPlants : Items
{
    [Serializable]
    private struct Recipes
    {

    }

    [SerializeField] private List<Recipes> recipeList;
}
