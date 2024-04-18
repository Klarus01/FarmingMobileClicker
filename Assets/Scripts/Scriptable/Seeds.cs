using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Resources/Seeds")]
public class Seeds : Resources
{
    [SerializeField] private Plants plant;

    [SerializeField] private List<Sprite> plantStadiumSprite;

    public Plants Plant { get { return plant; } }
    public List<Sprite> PlantStadiumSprites { get {  return plantStadiumSprite; } }
}
