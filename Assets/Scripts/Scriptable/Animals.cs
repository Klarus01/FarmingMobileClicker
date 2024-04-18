using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Resources/Animals")]
public class Animals : Items
{
    [SerializeField] private Plants foodType;

    public Plants FoodType { get { return foodType; } }
}
