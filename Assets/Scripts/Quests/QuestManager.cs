using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestData> allExistingQuests = new();
    private List<QuestData> acceptedQuests = new();
    private List<QuestData> availableQuests = new();
    private float questRefreshTime = 300f;
    private float timeSinceLastQuest;

    void Start()
    {
        GenerateNewQuest();
    }

    void Update()
    {
        timeSinceLastQuest += Time.deltaTime;
        if (timeSinceLastQuest >= questRefreshTime)
        {
            GenerateNewQuest();
            timeSinceLastQuest = 0f;
        }
    }

    void GenerateNewQuest()
    {
        int randomIndex = Random.Range(0, allExistingQuests.Count);
        QuestData newQuest = allExistingQuests[randomIndex];
        availableQuests.Add(newQuest);
    }

    public void AcceptQuest(QuestData quest)
    {
        //TODO
        acceptedQuests.Add(quest);
    }

    public void RejectQuest(QuestData quest)
    {
        //TODO
        availableQuests.Remove(quest);
    }
}
