using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zmieniæ nazwê klasy

[CreateAssetMenu(menuName="UnlockedItems")]
public class UnlockedItems : ScriptableObject
{
    [Serializable]
    public struct Unlocked
    {
        [SerializeField] public ItemsData items;
        [SerializeField] public int count;

        public ItemsData Items { get { return items; } }
        public int Count { get { return count; } }
    }

    public List<Unlocked> unlockedList;
}
