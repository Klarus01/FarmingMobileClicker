using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Building/Farm")]
public class FarmData : ItemsData
{
    [SerializeField] private int animalCapacity;
    [SerializeField] private AnimalsData animalType;

    public int AnimalCapacity { get { return animalCapacity; } }
    public AnimalsData AnimalType { get { return animalType; } }
}
