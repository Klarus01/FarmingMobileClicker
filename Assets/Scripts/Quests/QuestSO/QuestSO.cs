using UnityEngine;

[CreateAssetMenu(menuName = "Quests/NewQuest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public string questDescription;
    public int rewardGold;
    public int rewardExp;
}