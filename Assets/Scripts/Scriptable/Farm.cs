using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Building/Farm")]
public class Farm : Items
{
    [SerializeField] private int animalCapacity;
    [SerializeField] private Animals animalType;

    public int AnimalCapacity { get { return animalCapacity; } }
    public Animals AnimalType { get { return animalType; } }
}
