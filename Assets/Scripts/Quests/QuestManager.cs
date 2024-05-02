using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestData> allExistingQuests = new();
    [SerializeField] private List<AcceptedQuest> acceptedQuestObject = new();
    [SerializeField] private List<AvailableQuests> availableQuestsObject = new();
    private List<QuestData> acceptedQuests = new();
    private List<QuestData> availableQuests = new();
    private float questRefreshTime = 5f;
    private float timeSinceLastQuest;

    public List<QuestData> AcceptedQuest => acceptedQuests;

    private void Start()
    {
        GenerateNewQuest();
    }

    private void Update()
    {
        if (availableQuests.Count.Equals(3)) return;
        timeSinceLastQuest += Time.deltaTime;
        if (timeSinceLastQuest >= questRefreshTime)
        {
            GenerateNewQuest();
            timeSinceLastQuest = 0f;
        }
    }

    private void GenerateNewQuest()
    {
        int randomIndex = Random.Range(0, allExistingQuests.Count);
        QuestData newQuest = allExistingQuests[randomIndex];
        availableQuests.Add(newQuest);
        
        foreach (var quest in availableQuestsObject)
        {
            if (quest.QuestData != null) continue;
            quest.gameObject.SetActive(true);
            quest.NewQuestInit(newQuest);
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
}