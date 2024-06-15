using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private List<QuestData> allExistingQuests = new();
    [SerializeField] private List<AcceptedQuest> acceptedQuestObject = new();
    [SerializeField] private List<AvailableQuests> availableQuestsObject = new();
    private List<QuestData> acceptedQuests = new();
    private List<QuestData> availableQuests = new();
    private float questRefreshTime = 5f;
    private float timeSinceLastQuest;

    private List<UnlockedItems> unlockedsItem = new();
    private Dictionary<PlantsData, bool> isUnlockedByPlantsData = new Dictionary<PlantsData, bool>();

    public List<QuestData> AcceptedQuest => acceptedQuests;

    private void Start()
    {
        player.OnLevelUp += UnlockPlant;
        GenerateNewQuest();
    }

    private void Update()
    {
        if (availableQuests.Count.Equals(3)) return;
        timeSinceLastQuest += Time.deltaTime;
        if (timeSinceLastQuest >= questRefreshTime)
        {
            GenerateNewQuest(); 
        }
    }

    private void GenerateNewQuest()
    {
        int randomIndex = Random.Range(0, allExistingQuests.Count);
        QuestData newQuest = allExistingQuests[randomIndex];
        availableQuests.Add(newQuest);

        List<PlantsData> plants = new List<PlantsData>();

        for(int i = 0; i < newQuest.RequestList.Count; i++)
        {
            plants.Add(newQuest.RequestList[i].plants);
        }

        foreach(PlantsData plantsData in plants)
        {
            if (isUnlockedByPlantsData[plantsData])
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
            if(unlockedItems.level != player.Level)
            {
                continue;
            }

            List<PlantsData> plantsDataList = new();

            for (int i = 0; i < unlockedItems.unlockedList.Count; i++)
            {
                if (unlockedItems.unlockedList[i].Items is not SeedsData)
                {
                    continue;
                }

                SeedsData seedsData = unlockedItems.unlockedList[i].Items as SeedsData;

                plantsDataList.Add(seedsData.Plant);
            }

            foreach (PlantsData plantsData in plantsDataList)
            {
                isUnlockedByPlantsData[plantsData] = true;
            }
        }
    }
}