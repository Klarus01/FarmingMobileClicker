using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="UnlockedItems")]
public class UnlockedItems : ScriptableObject
{
    [Serializable]
    public struct Unlocked
    {
        [SerializeField] public Items items;
        [SerializeField] public int count;

        public Items Items { get { return items; } }
        public int Count { get { return count; } }
    }

    public List<Unlocked> unlockedList;
}
