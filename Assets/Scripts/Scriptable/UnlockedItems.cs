using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zmienić nazwę klasy

[CreateAssetMenu(menuName="UnlockedItems")]
public class UnlockedItems : ScriptableObject
{
    [Serializable]
    public struct Unlocked
    {
        public ItemsData items;
        public int count;

        public ItemsData Items { get { return items; } }
        public int Count { get { return count; } }
    }

    public int level;
    public List<Unlocked> unlockedList;
}
