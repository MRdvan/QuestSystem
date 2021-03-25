using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventGoal
{
    public string requiredTag;
    public int currentAmount;
    public int requiredAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(string tag)
    {
        if (tag == requiredTag)
        {
            currentAmount++;
        }
    }
    public void Gathered(string tag)
    {
        if (tag == requiredTag)
        {
            currentAmount++;
        }
    }
    public void Talked(string tag)
    {
        if (tag == requiredTag)
        {
            currentAmount++;
        }
    }
}


