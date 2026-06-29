using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Script untuk handle aktivasi dialog*/
public class DialogueActivator : MonoBehaviour, Iinteractable
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private UnityEvent onDialogueStarted;
    private PlayerMovementScript player;
    private DialogueData defaultDialogue;

    private DialogueData runtimeDialogue;
    void Awake()
    {
        runtimeDialogue = Instantiate(dialogueData);
        defaultDialogue = Instantiate(dialogueData);
        var events = GetComponents<DialogueResponseEvent>();

        Debug.Log($"Found {events.Length} DialogueResponseEvents");

        foreach (var e in events)
        {
            Debug.Log(e.name + " -> " + e.DialogueData.name);
        }
    }
    public void SetNewDialogue(DialogueData newdialogue)
    {
        runtimeDialogue = newdialogue;
        AddDialogueResponseEvents(player);
    }
    public void ResetDialogue()
    {
        runtimeDialogue = Instantiate(defaultDialogue);
    }
    public void SetResponseDialogue(int index, DialogueData nextDialogue)
    {
        if (index < 0 || index >= runtimeDialogue.Responses.Length)
            return;

        runtimeDialogue.Responses[index].SetDialogue(nextDialogue);
        AddDialogueResponseEvents(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            Debug.Log("collided!!");
            player.Interactable = this;
            this.player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                this.player = null;
            }

        }
    }

    //function bdy dari interface
    // public void Interact(PlayerMovementScript player)
    // {
    //     //Debug.Log("Interacted"+dialogueData);
    //     onDialogueStarted?.Invoke();

    //     if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueData == dialogueData)
    //     {
    //         player.DialogueUI.AddResponseEvents(responseEvent.Events);
    //     }

    //     player.DialogueUI.showDialogue(dialogueData, dialogueData.Dialoguepicleft, dialogueData.Dialoguepicright);
    // }

    public void Interact(PlayerMovementScript player)
    {
        onDialogueStarted?.Invoke();
        AddDialogueResponseEvents(player);
        player.DialogueUI.showDialogue(runtimeDialogue, runtimeDialogue.Dialoguepicleft, runtimeDialogue.Dialoguepicright);
    }

    private void AddDialogueResponseEvents(PlayerMovementScript player)
    {


        foreach (var responseEvent in GetComponents<DialogueResponseEvent>())
        {
            Debug.Log(responseEvent);

            Debug.Log(responseEvent.DialogueData.Dialogue[0] == runtimeDialogue.Dialogue[0]);
            if (responseEvent.DialogueData.Dialogue[0] == runtimeDialogue.Dialogue[0])
            {
                Debug.Log(responseEvent.name + " -> " + responseEvent.DialogueData.name);
                player.DialogueUI.AddResponseEvents(responseEvent.Events);
                break;
            }
            // if (responseEvent.DialogueData.Dialogue[0] != runtimeDialogue.Dialogue[0])
            //     continue;

            // player.DialogueUI.AddResponseEvents(responseEvent.Events);
            // break;
        }
    }
}
