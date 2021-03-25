using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public Button buttonComponent;
    public RawImage icon;
    public Text eventName;
    public Sprite waitingIcon;
    public Sprite currentIcon; 
    public Sprite doneIcon;
    public QuestEvent thisEvent;
    CompassController compassController; 
    QuestEvent.EventStatus status;

    private void Awake()
    {
        buttonComponent.onClick.AddListener(ClickHandler);
        compassController = CompassController.Instance;
    }

    private void ClickHandler()
    {
        compassController.target = thisEvent.location;
        QuestManager.Instance.UpdateWorkingEvent(thisEvent);
    }

    public void Setup(QuestEvent e,GameObject scrollList)
    {
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
        status = thisEvent.status;
        icon.texture = waitingIcon.texture;
        buttonComponent.interactable = false;//cant clickable
    }

    
    public void UpdateButton(QuestEvent.EventStatus s)
    {
        status = s;
        switch (status)
        {
            case QuestEvent.EventStatus.WAITING:
                icon.texture = waitingIcon.texture;
                icon.color = Color.red;
                buttonComponent.interactable = false;
                break;
            case QuestEvent.EventStatus.CURRENT:
                icon.texture = currentIcon.texture;
                icon.color = Color.yellow;
                buttonComponent.interactable = true;
                ClickHandler();
                break;
            case QuestEvent.EventStatus.DONE:
                icon.texture = doneIcon.texture;
                icon.color = Color.green;
                buttonComponent.interactable = false;
                break;
            default:
                break;
        }
    }



}
