using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : ItemsData
{
    [SerializeField] private float timeToCreate;
    [SerializeField] private int expFromCollect;

    public float TimeToCreate => timeToCreate;
    public int ExpFromCollect => expFromCollect;
}
