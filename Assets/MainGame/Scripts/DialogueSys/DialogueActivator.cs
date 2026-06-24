using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script untuk handle aktivasi dialog*/
public class DialogueActivator : MonoBehaviour, Iinteractable
{
    public MobData nPC;
    /*private SpriteRenderer npcSr;
    private SpriteRenderer bgSr;*/
    private DialogueData dialogueData { get; set; }

    public GameObject background;
    private void Start()
    {
        /*        bgSr = background.GetComponent<SpriteRenderer>();
                npcSr = GetComponent<SpriteRenderer>();*/
        dialogueData = nPC.npcDialogue;
        Debug.Log(nPC.name);
        Debug.Log(dialogueData);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            Debug.Log("collided!!");
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }

        }
    }

    public void UpdateDialogueObject(MobData nPC)
    {
        dialogueData = nPC.npcDialogue;
        dialogueData.Dialoguepicleft = nPC.npcDialogue.Dialoguepicleft;
        //sDebug.Log(dialogueData, dialogueData.Dialoguepic);
    }

    //function bdy dari interface
    public void Interact(PlayerMovementScript player)
    {
        //Debug.Log("Interacted"+dialogueData);

        if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueData == dialogueData)
        {
            player.DialogueUI.AddResponseEvents(responseEvent.Events);
        }

        player.DialogueUI.showDialogue(dialogueData, dialogueData.Dialoguepicleft, dialogueData.Dialoguepicright);
    }
}
