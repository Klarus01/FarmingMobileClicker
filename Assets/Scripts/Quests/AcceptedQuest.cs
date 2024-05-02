using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedQuest : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text numberOfItemsNeededText;
    [SerializeField] private Button doneButton; 
    private QuestData questData;
    public QuestData QuestData => questData;

    private void Start()
    {
        doneButton.onClick.AddListener(DoneQuest);
    }

    public void NewQuestInit(QuestData newQuestData)
    {
        questData = newQuestData;

        if (!questData) return;
        questNameText.SetText(questData.questName);
        numberOfItemsNeededText.SetText("X/" + questData.RequestList[0].plantCount); //change X to amount from player eq
    }

    private void DoneQuest()
    {
        Debug.Log("Quest Done");
    }

    private void AddRewardToPlayer()
    {
        //TODO
    }
}