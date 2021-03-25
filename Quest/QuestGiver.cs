using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quests;
    public  GameObject compass;

    void Start()
    {
        for (int q = 0; q < quests.Count; q++)
        {
            //connecting each event to its next event by passing through all of em
            for (int i = 0; i < quests[q].questEvents.Count; i++)
            {
                for (int j = 0; j < quests[q].questEvents[i].nextEvents.Count; j++)
                {
                    quests[q].AddPath(quests[q].questEvents[i].GetId(), quests[q].questEvents[i].nextEvents[j]);
                }
            }
        }
        
    }

    //activating the quest 
    private void OnTriggerEnter(Collider other)//will call with button click by player
    {
        if (other.gameObject.CompareTag("archer"))
        {
            if (quests.Count > 0 && QuestManager.Instance.quest == null)
            {
                QuestManager.Instance.GiveQuestToPlayer(quests[quests.Count - 1], gameObject);
            }
        }
        
    }


}
