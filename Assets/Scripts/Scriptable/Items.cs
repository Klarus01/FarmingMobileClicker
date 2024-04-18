using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : ScriptableObject
{
    [SerializeField] private int itemValue;

    [SerializeField] private Sprite itemSprite;

    public int ItemValue { get { return itemValue; } }
    public Sprite ItemSprite { get {  return itemSprite; } }
}
