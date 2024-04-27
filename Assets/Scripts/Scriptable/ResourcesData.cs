using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : ItemsData
{
    [SerializeField] private float timeToCreate;
    [SerializeField] private float expFromCollect;

    public float TimeToCreate { get { return timeToCreate; } }
    public float ExpFromCollect { get {  return expFromCollect; } }
}
