using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/NewQuest")]
public class QuestData : ScriptableObject
{
    [Serializable]
    public struct Request
    {
        public PlantsData plants;
        public int plantCount;
    }

    [SerializeField] private List<Request> requestList = new();
    public string questName;
    public string questDescription;
    public int rewardGold;
    public int rewardExp;

    public List<Request> RequestList => requestList;
}