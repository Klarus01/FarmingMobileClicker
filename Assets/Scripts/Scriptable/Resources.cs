using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : Items
{
    [SerializeField] private float timeToCreate;
    [SerializeField] private float expFromCollect;

    public float TimeToCreate { get { return timeToCreate; } }
    public float ExpFromCollect { get {  return expFromCollect; } }
}
