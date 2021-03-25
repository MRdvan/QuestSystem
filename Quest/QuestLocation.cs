using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestManager qManager;
    //public List<QuestEvent> qEvents = new List<QuestEvent>();
    public QuestButton qButton;
    bool check = false;
    public QuestEvent qEvent;


    //
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag != "archer") return;//if not archer reach
        if (qEvent == null) return;
        
        if (qEvent.status == QuestEvent.EventStatus.CURRENT)
        {
            if (other.gameObject.tag == "archer")
            {
                if (qEvent.Type == GoalType.Talk)
                {
                    QuestManager.Instance.UpdateEventGoalProgress(gameObject.tag, GoalType.Talk);
                }
                for (int j = 0; j < qEvent.eventGoals.Count; j++)
                {
                    if (qEvent.eventGoals[j].isReached())
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {

                    qEvent.updateQuestEvent(QuestEvent.EventStatus.DONE);
                    qButton.UpdateButton(QuestEvent.EventStatus.DONE);
                    qManager.UpdateQuestOnCompletion(qEvent);
                    check = false;
                }
            }

        }

        
        
    }

    public void Setup(QuestManager qm, QuestEvent qe,QuestButton qb)
    {
        qManager = qm;
        qButton = qb;
        qEvent = qe;
        
        if (qEvent!=null)
        {
            qEvent.button = qb;
        }
        
        
    }
}
