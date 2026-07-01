using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Script untuk handle aktivasi dialog*/
public class DialogueActivator : MonoBehaviour, Iinteractable
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private UnityEvent onDialogueStarted;
    [SerializeField] private UnityEvent onDialogueEnd;
    private DialogueData defaultDialogue;

    private DialogueData runtimeDialogue;
    void Awake()
    {
        runtimeDialogue = Instantiate(dialogueData);
        defaultDialogue = Instantiate(dialogueData);
    }
    public void SetNewDialogue(DialogueData newdialogue)
    {
        runtimeDialogue = newdialogue;
        AddDialogueResponseEvents();
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
        AddDialogueResponseEvents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }

        }
    }

    //function bdy dari interface
    // public void Interact(PlayerController player)
    // {
    //     //Debug.Log("Interacted"+dialogueData);
    //     onDialogueStarted?.Invoke();

    //     if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueData == dialogueData)
    //     {
    //         player.DialogueUI.AddResponseEvents(responseEvent.Events);
    //     }

    //     player.DialogueUI.showDialogue(dialogueData, dialogueData.Dialoguepicleft, dialogueData.Dialoguepicright);
    // }

    public void Interact(PlayerController player)
    {
        onDialogueStarted?.Invoke();
        AddDialogueResponseEvents();
        player.Lock(this);
        dialogueUI.ShowDialogue(runtimeDialogue, runtimeDialogue.Dialoguepicleft, runtimeDialogue.Dialoguepicright, () =>
        {
            player.Unlock(this);
            onDialogueEnd?.Invoke();
        });
    }

    private void AddDialogueResponseEvents()
    {


        foreach (var responseEvent in GetComponents<DialogueResponseEvent>())
        {
            if (responseEvent.DialogueData.Dialogue[0] == runtimeDialogue.Dialogue[0])
            {
                dialogueUI.AddResponseEvents(responseEvent.Events);
                break;
            }
            // if (responseEvent.DialogueData.Dialogue[0] != runtimeDialogue.Dialogue[0])
            //     continue;

            // dialogueUI.AddResponseEvents(responseEvent.Events);
            // break;
        }
    }
}
