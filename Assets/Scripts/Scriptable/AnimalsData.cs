using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Resources/Animals")]
public class AnimalsData : ItemsData
{
    [SerializeField] private PlantsData foodType;

    public PlantsData FoodType { get { return foodType; } }
}
