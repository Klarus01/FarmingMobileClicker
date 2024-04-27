using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Resources/Seeds")]
public class SeedsData : ResourcesData
{
    [SerializeField] private PlantsData plant;

    [SerializeField] private List<Sprite> plantStadiumSprite;

    public PlantsData Plant { get { return plant; } }
    public List<Sprite> PlantStadiumSprites { get {  return plantStadiumSprite; } }
}
