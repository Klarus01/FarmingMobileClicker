using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestSO> allExistingQuests = new();
    private List<QuestSO> acceptedQuests = new();
    private List<QuestSO> availableQuests = new();
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
        QuestSO newQuest = allExistingQuests[randomIndex];
        availableQuests.Add(newQuest);
    }

    public void AcceptQuest(QuestSO quest)
    {
        //TODO
        acceptedQuests.Add(quest);
    }

    public void RejectQuest(QuestSO quest)
    {
        //TODO
        availableQuests.Remove(quest);
    }
}
