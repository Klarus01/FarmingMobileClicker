using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<UnlockedItems> unlockedsItem;

    [SerializeField] private List<QuestData> allExistingQuests = new();
    [SerializeField] private List<AcceptedQuest> acceptedQuestObject = new();
    [SerializeField] private List<AvailableQuests> availableQuestsObject = new();
    private List<QuestData> acceptedQuests = new();
    private List<QuestData> availableQuests = new();
    private float questRefreshTime = 5f;
    private float timeSinceLastQuest;

    private List<QuestData> possibleQuest = new();

    private Dictionary<PlantsData, bool> isUnlockedByPlantsData = new Dictionary<PlantsData, bool>();

    public List<QuestData> AcceptedQuest => acceptedQuests;

    private void Awake()
    {
        Player.Instance.OnLevelUp += UnlockPlant;
    }

    private void Start()
    {
        UnlockPlant();
    }

    private void Update()
    {
        if(possibleQuest.Count == 0)
        {
            return;
        }

        if (availableQuests.Count.Equals(3)) return;
        timeSinceLastQuest += Time.deltaTime;
        if (timeSinceLastQuest >= questRefreshTime)
        {
            GenerateNewQuest(); 
        }
    }

    private void GenerateNewQuest()
    {
        int randomIndex = Random.Range(0, possibleQuest.Count);
        Debug.Log(randomIndex);
        QuestData newQuest = possibleQuest[randomIndex];
        availableQuests.Add(newQuest);

        List<PlantsData> plants = new List<PlantsData>();

        for(int i = 0; i < newQuest.RequestList.Count; i++)
        {
            plants.Add(newQuest.RequestList[i].plants);
        }

        if(plants.Count == 0)
        {
            return;
        }

        foreach(PlantsData plantsData in plants)
        {
            if (isUnlockedByPlantsData.ContainsKey(plantsData))
            {
                continue;
            }

            return;
        }

        foreach (var quest in availableQuestsObject)
        {
            if (quest.QuestData != null) continue;
            quest.gameObject.SetActive(true);
            quest.NewQuestInit(newQuest);
            timeSinceLastQuest = 0f;
            return;
        }
    }

    public void QuestRejected(QuestData questToRemove)
    {
        availableQuests.Remove(questToRemove);
    }

    public void QuestAccepted(QuestData questToAccept)
    {
        acceptedQuests.Add(questToAccept);
        availableQuests.Remove(questToAccept);
        foreach (var quest in acceptedQuestObject)
        {
            if (quest.QuestData != null) continue;
            quest.gameObject.SetActive(true);
            quest.NewQuestInit(questToAccept);
            return;
        }
    }

    public void RewardFromQuest(QuestData questDone)
    {
        Player.Instance.AddExp(questDone.rewardExp);
        acceptedQuests.Remove(questDone);
    }

    private void UnlockPlant()
    {
        foreach(UnlockedItems unlockedItems in unlockedsItem)
        {
            if(unlockedItems.level != Player.Instance.Level)
            {
                continue;
            }

            List<PlantsData> plantsDataList = new();

            for (int i = 0; i < unlockedItems.unlockedList.Count; i++)
            {
                if (unlockedItems.unlockedList[i].Items is not PlantsData)
                {
                    continue;
                }

                PlantsData seedsData = unlockedItems.unlockedList[i].Items as PlantsData;

                plantsDataList.Add(seedsData);
            }

            foreach (PlantsData plantsData in plantsDataList)
            {
                isUnlockedByPlantsData.Add(plantsData, true);
                Debug.Log(plantsData.name);
            }
        }

        UnlockQuest();
    }
    private void UnlockQuest()
    {
        foreach(QuestData questData in allExistingQuests)
        {
            for(int i = 0; i < questData.RequestList.Count; i++)
            {
                if (!isUnlockedByPlantsData.ContainsKey(questData.RequestList[i].plants))
                {
                    break;
                }

                if(possibleQuest.Contains(questData))
                {
                    return;
                }

                possibleQuest.Add(questData);
            }
        }
    }
}