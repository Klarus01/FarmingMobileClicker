using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zmieni� nazw� klasy

[CreateAssetMenu(menuName = "Items/Building/ProcessingPlants")]
public class ProcessingPlants : ItemsData
{
    [Serializable]
    private struct Recipes
    {

    }

    [SerializeField] private List<Recipes> recipeList;
}
