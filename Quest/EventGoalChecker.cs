using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGoalChecker : MonoBehaviour
{
    public QuestManager questManager;
    public List<GameObject> enemies;
    public List<GameObject> collectables;
    public List<GameObject> talkables;
    QuestEvent workingEvent;

    // Start is called before the first frame update
    void Start()
    {
        questManager = QuestManager.Instance;
        workingEvent = questManager.workingEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEventGoalProgress(string tag, GoalType type)//update events goal when a progress happen
    {
        for (int i = 0; i < workingEvent.eventGoals.Count; i++)
        {
            switch (type)
            {
                case GoalType.Kill:
                    workingEvent.eventGoals[i].EnemyKilled(tag);//increase kill counter for each type of enemy
                    break;
                case GoalType.Gather:
                    workingEvent.eventGoals[i].Gathered(tag);//increase gather counter for each type of item
                    break;
                case GoalType.Talk:
                    workingEvent.eventGoals[i].Talked(tag);//increase gather counter for each type of item
                    break;
                default:
                    break;
            }
        }

    }

}
