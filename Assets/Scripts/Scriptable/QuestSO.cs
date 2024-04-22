using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/NewQuest")]
public class QuestSO : ScriptableObject
{
    [Serializable]
    public struct Request
    {
        public Plants plants;
        public int plantCount;
    }

    [SerializeField] private List<Request> requestList = new();
    public string questName;
    public string questDescription;
    public int rewardGold;
    public int rewardExp;

    public List<Request> RequestList { get { return requestList; } }
}