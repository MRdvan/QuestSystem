using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class QuestEvent
{
    public string name;
    public string id;
    public string description;
    public GoalType Type;
    public List<EventGoal> eventGoals;
    public  GameObject location;
    public List<string> nextEvents;
    
    public List<QuestPath> pathList = new List<QuestPath>();
    [NonSerialized]public int order = -1;
    public enum EventStatus { WAITING, CURRENT, DONE };
    [NonSerialized] public EventStatus status = EventStatus.WAITING;
    [NonSerialized] public QuestButton button;

    public void updateQuestEvent(EventStatus newStatus)
    {
        status = newStatus;
        button.UpdateButton(newStatus);
    }

    public string GetId()
    {
        return id;
    }
}



public enum GoalType
{
    Kill, Gather, Talk
}
