using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region Singleton
    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public Quest quest;//take it from npc or something else
    public GameObject questGiver;
    public GameObject questPrintBox;//views of events of the quest
    public GameObject buttonPrefab;//event button
    QuestButton button;
    public GameObject archer;
    public QuestEvent workingEvent;
    public GameObject compass;

    QuestEvent final;//last event to complete all quest

    private void Start()
    {
        quest = null;
    }

    public void GiveQuestToPlayer(Quest q,GameObject giver)//takes quest
    {
        quest = q;
        questGiver = giver;
        workingEvent = quest.questEvents[0];
        

        quest.BFS(workingEvent.GetId());//make all events of the quest in order with BFS(shape of graph)

        ShowQuestEvents();//fill the scrollview with events

        UpdateWorkingEvent(workingEvent);//update the current event
        CompassController.Instance.target = workingEvent.location;

        quest.PrintPath();//prints all events

        final = quest.questEvents[quest.questEvents.Count - 1];//sets the final event for finish quest.
    }

    public void ShowQuestEvents()
    {
        for (int i = 0; i < quest.questEvents.Count; i++)//passing through each event of the quest
        {
            button = CreateButton(quest.questEvents[i]).GetComponent<QuestButton>();//create a button for the event
            quest.questEvents[i].location.GetComponent<QuestLocation>().Setup(this, quest.questEvents[i], button);//setup the location
        }
    }

    public void DestroyCompletedQuestEvents()
    {
        for (int i = 0; i < quest.questEvents.Count; i++)
        {
            quest.questEvents[i].location.GetComponent<QuestLocation>().Setup(null, null, null);//setup the location
            quest.questEvents[i].button.transform.SetParent(null);
            Destroy(quest.questEvents[i].button.gameObject);
        }
        quest = null;
        workingEvent = null;
        CompassController.Instance.pointer.SetActive(false);
        CompassController.Instance.target = null;

    }

    

    public void UpdateWorkingEvent(QuestEvent q)
    {
        if (q.status == QuestEvent.EventStatus.CURRENT)
        {
            workingEvent = q;
            workingEvent.location.GetComponent<QuestLocation>().Setup(this, q, q.button);
        }
    }

    public void UpdateEventGoalProgress(string tag,GoalType type)//update events goal when a progress happen
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

    public GameObject CreateButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().Setup(e, questPrintBox);
        if (e.order == 1)//makes the first event the current one
        {
            b.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
            workingEvent = e;
            //workingEvent.location.GetComponent<QuestLocation>().Setup(this, workingEvent, workingEvent.button);

        }
        return b;
    }

    public void UpdateQuestOnCompletion(QuestEvent qEvent)
    {

        foreach (QuestEvent qe in quest.questEvents)
        {
            //if this event is the next in order
            if (qe.order == (qEvent.order+1))
            {
                qe.updateQuestEvent(QuestEvent.EventStatus.CURRENT);
                UpdateWorkingEvent(qe);
            }
        }

        if (final == qEvent)
        {
            Debug.Log("quest is completed");
            archer.GetComponent<ArcherInventory>().CollectRewards(quest.xpReward, quest.goldReward, quest.itemRewards);
            questGiver.GetComponent<QuestGiver>().quests.RemoveAt(questGiver.GetComponent<QuestGiver>().quests.Count - 1);
            DestroyCompletedQuestEvents();
        }
    }

}
