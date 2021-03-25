using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public float xpReward; 
    public int goldReward;
    public List<GameObject> itemRewards;
    public List<QuestEvent> questEvents;

    public void AddPath(string fromQuestEvent, string toQuestEvent)
    {
        QuestEvent from = FindQuestEvent(fromQuestEvent);
        QuestEvent to = FindQuestEvent(toQuestEvent);

        if (from != null && to != null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathList.Add(p);
        }
    }

    private QuestEvent FindQuestEvent(string id)
    {
        foreach (QuestEvent n in questEvents)
        {
            if (n.GetId() == id)
                return n;
        }
        return null;
    }

    public void BFS(string id,int orderNumber = 1)
    {
        QuestEvent thisEvent = FindQuestEvent(id);

        thisEvent.order = orderNumber;
        foreach (QuestPath e in thisEvent.pathList)
        {
            if (e.endEvent.order == -1)
            {
                BFS(e.endEvent.GetId(), orderNumber+1);
            }
        }
    }

    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.name + "order:" + n.order);
        }
    }
}
