using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script untuk handle aktivasi dialog otomatis*/
public class AutoDialogueActivator : MonoBehaviour
{
    public DialogueData startingDialogue;
    private DialogueData dialogueData { get; set; }
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField]
    private bool oneShot = true;
    private bool hasTriggered;

    private void Start()
    {

        dialogueData = startingDialogue;
        Debug.Log(dialogueData);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered && oneShot)
        {
            return;
        }
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController _))
        {
            hasTriggered = true;
            Debug.Log("collided!!");

            if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueData == dialogueData)
            {
                dialogueUI.AddResponseEvents(responseEvent.Events);
            }

            dialogueUI.showDialogue(dialogueData, dialogueData.Dialoguepicleft, dialogueData.Dialoguepicright);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            return;
        }
    }
}
