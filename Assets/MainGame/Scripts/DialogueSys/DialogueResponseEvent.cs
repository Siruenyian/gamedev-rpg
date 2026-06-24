using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueResponseEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private ResposeEvent[] events;
    DialogueActivator dialogueActivator;
    public DialogueData DialogueData => dialogueData;

    public ResposeEvent[] Events => events;

    private void Start()
    {
        dialogueActivator = this.GetComponent<DialogueActivator>();
        //dialogueData = dialogueActivator.nPC.npcDialogue;
    }

    public void OnValidate()
    {
        if (dialogueData == null)
        {
            return;
        }
        if (dialogueData.Responses == null)
        {
            return;
        }
        if (events != null && events.Length == dialogueData.Responses.Length)
        {
            return;
        }

        if (events == null)
        {
            events = new ResposeEvent[dialogueData.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueData.Responses.Length);
        }

        for (int i = 0; i < dialogueData.Responses.Length; i++)
        {
            Response response = dialogueData.Responses[i];
            if (events[i] != null)
            {
                events[i].name = response.Responsetext;
                continue;
            }
            events[i] = new ResposeEvent() { name = response.Responsetext };
        }
    }
}
